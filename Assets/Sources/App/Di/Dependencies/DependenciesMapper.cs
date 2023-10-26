using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sources.App.Di.Attributes;

namespace Sources.App.Di.Dependencies
{
    public class DependenciesMapper
    {
        private Dictionary<Type, Type> _provides = new Dictionary<Type, Type>();

        public DependenciesMapper() =>
            ImportDependencies();

        public Type Get(Type type) =>
            _provides.ContainsKey(type)
                ? _provides[type]
                : throw new NullReferenceException($"{GetType()}: not found dependency for {type}");

        private void ImportDependencies()
        {
            var dependencies = GetDependenciesClasses();

            if (dependencies.Length == 0)
                throw new NullReferenceException(
                    $"{GetType()}: not found class with {nameof(DependencyProvidesAttribute)}");

            if (dependencies.Length > 1)
                throw new AggregateException(
                    $"{GetType()}: found more then one class with {nameof(DependencyProvidesAttribute)}");

            ImportProvidesFrom(dependencies[0]);
        }

        private void ImportProvidesFrom(Type dependency)
        {
            if (ValidateDependencies(dependency))
                _provides = GetProvides(dependency);
        }

        private bool ValidateDependencies(Type type)
        {
            MethodInfo[] methods =
                type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                Type returnType = method.ReturnType;

                if (returnType.IsInterface == false)
                    throw new Exception($"CheckModule: return type of method '{method.Name}' must be an interface");

                if (parameters.Length != 1)
                    throw new Exception(
                        $"CheckModule: method '{method.Name}' from module '{type}' must contain only one parameter");

                Type dependency = parameters[0].ParameterType;

                if (dependency.GetInterfaces().Contains(returnType) == false)
                    throw new Exception(
                        $"CheckModule: parameter type '{dependency}' must realize '{returnType}' interface");
            }

            return true;
        }

        private Dictionary<Type, Type> GetProvides(Type moduleType)
        {
            Dictionary<Type, Type> provides = new Dictionary<Type, Type>();

            MethodInfo[] methods =
                moduleType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                Type firstParameter = parameters[0].ParameterType;
                provides.Add(method.ReturnType, firstParameter);
            }

            return provides;
        }

        private Type[] GetDependenciesClasses() =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetCustomAttribute(typeof(DependencyProvidesAttribute)) != null)
                .ToArray();
    }
}