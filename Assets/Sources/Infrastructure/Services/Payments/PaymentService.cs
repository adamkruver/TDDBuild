using System;
using Sources.Domain.Credits;
using UnityEngine;

namespace Sources.Infrastructure.Services.Payments
{
    public class PaymentService
    {
        private readonly Money _money;

        public PaymentService(Money money)
        {
            _money = money;
        }

        public event Action MoneyChanged; 

        public bool TryPay(int money)
        {
            if(money < 0) 
                return false; // TODO: add view

            if (IsEnough(money) == false)
                return false; // TODO: add view
            
            _money.Remove(money);
            MoneyChanged?.Invoke();
            
            return true;
        }
        
        public bool IsEnough(int money) => 
            _money.IsEnough(money);
    }
}