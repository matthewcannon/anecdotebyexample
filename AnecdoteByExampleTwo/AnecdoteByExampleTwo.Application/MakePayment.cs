﻿namespace AnecdoteByExampleTwo.Application
{
    public class MakePayment
    {
        public Payment Payment { get; private set; }

        public MakePayment(Payment payment)
        {
            Payment = payment;
        }
    }
}