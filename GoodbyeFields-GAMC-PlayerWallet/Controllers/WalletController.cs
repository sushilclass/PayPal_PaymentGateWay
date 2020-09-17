using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;


namespace GoodbyeFields_GAMC_PlayerWallet.Controllers
{
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IPlayerWalletService _walletService;
        private readonly ILogger _logger;

        public WalletController(IPlayerWalletService walletService, ILogger logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Deposit")]
        public ActionResult Deposit(TransactionModel model)
        {
            _logger.LogInformation("Deposite method called with request parameter : " + model);
            ResponseModel response = new ResponseModel();
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("check passed model is valid or not");
                    response = _walletService.Deposit(model);
                }
                else
                {
                    _logger.LogError("Invalid model");
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.InvalidData;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in deposite method, error message " + ex.Message);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            return Ok(response);
        }

        //to withdrawal balance
        [HttpPost]
        [Route("Withdrawal")]
        public ActionResult Withdrawal(TransactionModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = _walletService.Withdrawal(model);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.InvalidData;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            return Ok(response);
        }

        //transaction history of a player
        [HttpPost]
        [Route("TransactionHistory")]
        public ActionResult TransactionHistory(TransactionModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (model.PlayerId != "0")
                {
                    response = _walletService.TransactionHistory(model);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.InvalidData;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            return Ok(response);
        }

        //get balance of a player
        [HttpPost]
        [Route("GetBalance")]
        public ActionResult GetBalance(TransactionModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (model.PlayerId != "")
                {
                    response = _walletService.GetBalance(model.PlayerId);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.InvalidData;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            return Ok(response);
        }

        //void a transaction
        [HttpPost]
        [Route("VoidATransaction")]
        public ActionResult VoidATransaction(TransactionModel model)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (ModelState.IsValid)
                {
                    response = _walletService.VoidATransaction(model);
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.InvalidData;
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            return Ok(response);
        }
    }
}
