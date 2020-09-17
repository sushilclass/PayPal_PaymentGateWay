using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GoodbyeFields_GAMC_PaymentAPI.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        /// <summary>
        /// Add Service Name
        /// </summary>
        private IPlayerService _playerservice;

        /// <summary>
        /// 
        /// </summary>
        private PayPalService _paypalservice;

        /// <summary>
        /// Inject Service
        /// </summary>
        /// <param name="playerService"></param>
        /// <param name="payPalService"></param>
        public PlayerController(IPlayerService playerService, PayPalService payPalService,LogManagers.LogManagers logger)
        {
            _playerservice = playerService;
            _paypalservice = payPalService;
        }

        /// <summary>
        /// Authenticate player
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            StringBuilder log = new StringBuilder();
            string requestParameter = "Username = " + model.Username + ", " + "Password = " + model.Password ;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.Authenticate).Replace("{RequestParameter}", requestParameter));

            var response = _playerservice.Authenticate(model);
            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            else
            {
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Authenticate));
                LogManagers.LogManagers.WriteTraceLog(log);
                return Ok(response);

            }
        }

        /// <summary>
        /// ReturnURL from PayPal Execution
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ReturnURL")]
        public RedirectResult ReturnURL()
        {
            StringBuilder log = new StringBuilder();

            log.Append(UtilityResource.ReturnURLstart.Replace("{MethodName}", UtilityResource.ReturnURL));
            LogManagers.LogManagers.WriteTraceLog(log);

            string cancelRequest=Request.Query["cancel"];
            PayPalResponseModel model = new PayPalResponseModel();
            try
            {
                if (string.IsNullOrWhiteSpace(cancelRequest))
                {
                    var result = _paypalservice.ExecutePayment(Request.Query["PayerID"], Request.Query["paymentId"]);
                    model.Id = result.id; 
                    model.CreateTime = DateTime.Parse(result.create_time);
                    model.UpdateTime = DateTime.Parse(result.update_time);
                    model.Intent = result.intent;
                    model.PaymentMethod = result.payer.payment_method;
                    model.PayerId = result.payer.payer_info.payer_id;
                    model.Amount = decimal.Parse(result.transactions[0].amount.total.ToString());
                    model.Currency = result.transactions[0].amount.currency;
                    model.Description = result.transactions[0].description;
                    model.Status = result.state;
                    model.InvoiceNumber = result.transactions[0].invoice_number;
                    var JSONRes = JsonConvert.SerializeObject(result);
                    model.FullResponse = JSONRes;
                    var res = _playerservice.SavePayPalResponse(model);

                    log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.ReturnURL));
                    LogManagers.LogManagers.WriteTraceLog(log);
                    return RedirectToSuccess(model);                   
                }
                else
                {
                    log.Append(UtilityResource.ReturnToCancel.Replace("{MethodName}", UtilityResource.ReturnURL));
                    LogManagers.LogManagers.WriteTraceLog(log);
                    return RedirectToCancel(); 
                }               
            } 
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.ReturnURL).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
                return RedirectToFailed(ex.ToString());
            }
            
        }

        /// <summary>
        /// Checkout with PayPal
        /// </summary>
        /// <returns></returns>
        [HttpPost("PaymentWithPaypal")]
        public RedirectResult PaymentWithPaypal(Transaction model)
        {
            StringBuilder log = new StringBuilder();
            log.Append("Enter into PaymentWithPaypal method");
            var result = _paypalservice.CheckOut(model.TransactionAmount, model.TransactionDescription);
            RedirectResult redirectResult = new RedirectResult(result, true);
            log.Append("PaymentWithPaypal method execute successfuly.");
            LogManagers.LogManagers.WriteTraceLog(log);
            return redirectResult;
        }

        /// <summary>
        /// Return to Success
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("RedirectToSuccess")]
        public RedirectResult RedirectToSuccess(PayPalResponseModel model)
        {
            return RedirectPermanent(UtilityResource.PayPalSuccess + model.Id);
        }

        /// <summary>
        /// Return to Cancel
        /// </summary>
        /// <returns></returns>
        [HttpGet("RedirectToCancel")]
        public RedirectResult RedirectToCancel()
        {
            return RedirectPermanent(UtilityResource.PayPalCancel);
        }

        /// <summary>
        /// Return to failed
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [HttpGet("RedirectToFailed")]
        public RedirectResult RedirectToFailed(string ex)
        {
            return RedirectPermanent(UtilityResource.PayPalFailed + ex.ToString());
        }

    }
}