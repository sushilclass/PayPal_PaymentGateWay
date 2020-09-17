using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using System;
using System.Net;
using System.Text;

namespace GoodbyeFields_GAMC_WalletLibrary
{
    public class WalletMethods
    {
       /// <summary>
       /// global variables
       /// </summary>
        public IPlayerWalletService _walletService;

       /// <summary>
       /// initilize global variables
       /// </summary>
       /// <param name="walletService"></param>
        public WalletMethods(IPlayerWalletService walletService)
        {
            _walletService = walletService;
        }

        /// <summary>
        /// deposit amount in wallet (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Deposit(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            string response = string.Empty;

            string requestParameter = "TransactionAmount = " + model.TransactionAmount + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionDescription = " + model.TransactionDescription + ", " + "TransactionCode = " + model.TransactionCode;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.Deposit).Replace("{RequestParameter}", requestParameter));


            if (string.IsNullOrEmpty(model.TransactionAmount.ToString()) || model.TransactionAmount == 0 || model.TransactionAmount < 1)
            {
                log.Append(UtilityResource.TransactionAmountError);
                response = UtilityResource.TransactionAmountError;
                return response;
            }

            if (string.IsNullOrEmpty(model.PlayerId))
            {
                log.Append(UtilityResource.PlayerIdError);
                response = UtilityResource.PlayerIdError;
                return response;
            }

            if (string.IsNullOrEmpty(model.TransactionDescription))
            {
                model.TransactionDescription = string.Empty;
            }

            if (string.IsNullOrEmpty(model.TransactionCode))
            {
                log.Append(UtilityResource.TransactionCodeError);
                response = UtilityResource.TransactionCodeError;
                return response;
            }

            try
            {
                response = _walletService.Deposit(model);
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Deposit));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.Deposit).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
                response = ex.Message;
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return response;
        }

        /// <summary>
        /// withdrawal some amount frfom wallet (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Withdrawal(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            string response = string.Empty;

            string requestParameter = "TransactionAmount = " + model.TransactionAmount + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionDescription = " + model.TransactionDescription + ", " + "TransactionCode = " + model.TransactionCode;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.Withdrawal).Replace("{RequestParameter}", requestParameter));


            if (string.IsNullOrEmpty(model.TransactionAmount.ToString()) || model.TransactionAmount == 0)
            {
                log.Append(UtilityResource.TransactionAmountError);
                response = UtilityResource.TransactionAmountError;
                return response;
            }

            if (string.IsNullOrEmpty(model.PlayerId))
            {
                log.Append(UtilityResource.PlayerIdError);
                response = UtilityResource.PlayerIdError;
                return response;
            }

            if (string.IsNullOrEmpty(model.TransactionDescription))
            {
                model.TransactionDescription = string.Empty;
            }

            if (string.IsNullOrEmpty(model.TransactionCode))
            {
                log.Append(UtilityResource.TransactionCodeError);
                response = UtilityResource.TransactionCodeError;
                return response;
            }
            try
            {
                response = _walletService.Withdrawal(model);
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Withdrawal));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.Withdrawal).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
                response = ex.Message;
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return response;
        }

        /// <summary>
        /// to fetch the list or transactions for a particular player (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel TransactionHistory(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            ResponseModel response = new ResponseModel();

            string requestParameter = "TransactionFromDate = " + model.TransactionFromDate + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionToDate = " + model.TransactionToDate;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.TransactionHistory).Replace("{RequestParameter}", requestParameter));

            if (string.IsNullOrEmpty(model.PlayerId))
            {
                log.Append(UtilityResource.PlayerIdError);
               // response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = UtilityResource.PlayerIdError;
                response.Data = null;
                return response;
            }

            if (string.IsNullOrEmpty(model.TransactionFromDate.ToString()) || model.TransactionFromDate.ToString() == "1/1/0001 12:00:00 AM")
            {
                log.Append(UtilityResource.TransactionFromDateError);
               // response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = UtilityResource.TransactionFromDateError;
                response.Data = null;
                return response;
            }

            if (string.IsNullOrEmpty(model.TransactionToDate.ToString()) || model.TransactionToDate.ToString() == "1/1/0001 12:00:00 AM")
            {
                log.Append(UtilityResource.TransactionToDateError);
               // response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = UtilityResource.TransactionToDateError;
                response.Data = null;
                return response;
            }

            if (model.TransactionFromDate > model.TransactionToDate)
            {
                log.Append(UtilityResource.DateErrorMessage);
              //  response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = UtilityResource.DateErrorMessage;
                response.Data = null;
                return response;
            }

            try
            {
                response = _walletService.TransactionHistory(model);
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.TransactionHistory));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.TransactionHistory).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
               // response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return response;
        }

        /// <summary>
        /// get the balance of a particular player (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        public ResponseModel GetBalance(string PlayerId)
        {
            StringBuilder log = new StringBuilder();
            ResponseModel response = new ResponseModel();

            string requestParameter = "PlayerId = " + PlayerId;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.GetBalance).Replace("{RequestParameter}", requestParameter));

            if (string.IsNullOrEmpty(PlayerId))
            {
                log.Append(UtilityResource.PlayerIdError);
               // response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = UtilityResource.PlayerIdError;
                response.Data = null;
                return response;
            }

            try
            {
                response = _walletService.GetBalance(PlayerId);
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.GetBalance));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.GetBalance).Replace("{ErrorMessage}", ex.Message));
               // response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = UtilityResource.ErrorMessage;
                response.Description = ex.Message;
                response.Data = null;
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return response;
        }

        /// <summary>
        /// cancel a transaction (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string VoidTransaction(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            string response;

            string requestParameter = "TransactionId = " + model.TransactionId + ", VoidDescription = " + model.VoidDescription;
            log.Append(UtilityResource.LogStartMessage.Replace("{MethodName}", UtilityResource.VoidTransaction).Replace("{RequestParameter}", requestParameter));

            if (string.IsNullOrEmpty(model.TransactionId.ToString()) || model.TransactionId == 0)
            {
                log.Append(UtilityResource.TransactionIdError);
                response = UtilityResource.TransactionIdError;
                return response;
            }

            if (string.IsNullOrEmpty(model.VoidDescription))
            {
                model.VoidDescription = string.Empty;
            }

            try
            {
                response = _walletService.VoidATransaction(model);
                log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.VoidTransaction));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.VoidTransaction).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
                response = ex.Message;
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
            }
            return response;
        }
    }
}