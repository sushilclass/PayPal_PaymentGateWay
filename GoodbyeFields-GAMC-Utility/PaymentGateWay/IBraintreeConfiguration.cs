using Braintree;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Utility.PaymentGateWay
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
