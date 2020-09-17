using GoodbyeFields_GAMC_Utility;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_BL
{
    public class PayPalService
    {
        /// <summary>
        /// 
        /// </summary>
        private Payment payment;

        /// <summary>
        /// Execute the Payment method
        /// </summary>
        /// <param name="payerId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public Payment ExecutePayment(string payerId, string paymentId)
        {
            StringBuilder log = new StringBuilder();
            string requestParameter = "payerId = " + payerId + ", " + "paymentId = " + paymentId;
            log.Append("ExecutePayment method called with request parameters : " + requestParameter);

            Payment res = new Payment();
            try
            {
                APIContext apiContext = PaypalConfiguration.GetAPIContext();
                var paymentExecution = new PaymentExecution() { payer_id = payerId };
                this.payment = new Payment() { id = paymentId };

                log.Append("Execution starts");
                res = this.payment.Execute(apiContext, paymentExecution);
                log.Append("Execution ends");
                log.Append("ExecutePayment method executed successfully with response " + res);
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.ExecutePayment).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return res;
        }

        /// <summary>
        /// Create Payment method
        /// </summary>
        /// <param name="apiContext"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="payAmount"></param>
        /// <returns></returns>
        public Payment CreatePayment(APIContext apiContext, string redirectUrl, string payAmount, string Description)
        {
            StringBuilder log = new StringBuilder();
            string requestParameter = "payAmount = " + payAmount + ", " + "Description = " + Description + ", " + "apiContext = " + apiContext;
            log.Append("CreatePayment method called with request parameters : " + requestParameter);

            Payment res = null; // new Payment();
            try
            {
                //create itemlist and add item objects to it
                var itemList = new ItemList() { items = new List<Item>() };

                //Adding Item Details like name, currency, price etc
                itemList.items.Add(new Item()
                {
                    name = "Item Name",
                    currency = "USD",
                    price = payAmount,
                    quantity = "1",
                    sku = "sku"
                });

                log.Append("Created payer for payment");
                var payer = new Payer() { payment_method = "paypal" };


                log.Append("Redirect URL created");
                var redirUrls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };


                log.Append("Details created");
                var details = new Details()
                {
                    subtotal = payAmount
                };


                log.Append("Amount created");
                var amount = new Amount()
                {
                    currency = "USD",
                    total = payAmount,
                    details = details
                };

                string InvoiceNumber = "INVOICE_" + Guid.NewGuid().ToString();

                log.Append("Generated invoice number and created transaction list");
                var transactionList = new List<Transaction>();
                transactionList.Add(new Transaction()
                {
                    description = Description,
                    invoice_number = InvoiceNumber,
                    amount = amount,
                    item_list = itemList
                });

                log.Append("payment created");
                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrls
                };

                log.Append("payment create method called");
                res = this.payment.Create(apiContext);
                log.Append("create payment method executed successfully with res " + res);
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            catch (Exception ex)
            {
                LogManagers.LogManagers.WriteErrorLog(ex);
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return res;
        }

        /// <summary>
        /// Checkout method
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string CheckOut(decimal amount, string Description)
        {
            string paypalRedirectUrl = null;
            StringBuilder log = new StringBuilder();
            string requestParameter = "amount = " + amount + ", " + "Description = " + Description;
            log.Append("CheckOut method called with request parameters : " + requestParameter);
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                var guid = Convert.ToString((new Random()).Next(100000));
                string baseURI = UtilityResource.PayPalBaseURL + guid;
                var createdPayment = this.CreatePayment(apiContext, baseURI, amount.ToString(), Description);
                var links = createdPayment.links.GetEnumerator();                
                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }
                log.Append("Redirect Url is created and returned that URL in response");
            }
            catch (Exception ex)
            {
                LogManagers.LogManagers.WriteErrorLog(ex);
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return (paypalRedirectUrl);
        }
    }
}
