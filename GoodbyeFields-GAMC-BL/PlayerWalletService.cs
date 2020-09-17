using GoodbyeFields_GAMC_DataLayer;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;

namespace GoodbyeFields_GAMC_BL
{
    public class PlayerWalletService : IPlayerWalletService
    {
        /// <summary>
        /// global variable (Pragati jain 09-01-2020)
        /// </summary>
        private readonly GoodbyeFieldsGAMCDBContext _dbContext;

        /// <summary>
        /// initilize global variables
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="_logger"></param>
        public PlayerWalletService(GoodbyeFieldsGAMCDBContext dbContext, LogManagers.LogManagers _logger)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// deposit amount in wallet (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Deposit(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            string response;

            string requestParameter = "TransactionAmount = " + model.TransactionAmount + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionDescription = " + model.TransactionDescription + ", " + "TransactionCode = " + model.TransactionCode;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.Deposit).Replace("{RequestParameter}", requestParameter));

            try
            {
                this._dbContext.Database.OpenConnection();
                DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = UtilityResource.DepositSP;

                DbParameter TransactionAmount = cmd.CreateParameter();
                TransactionAmount.DbType = DbType.Decimal;
                TransactionAmount.ParameterName = "@TransactionAmount";
                TransactionAmount.Value = model.TransactionAmount;
                cmd.Parameters.Add(TransactionAmount);

                DbParameter playerID = cmd.CreateParameter();
                playerID.DbType = DbType.String;
                playerID.ParameterName = "@PlayerID";
                playerID.Value = model.PlayerId;
                cmd.Parameters.Add(playerID);

                DbParameter TransactionDescription = cmd.CreateParameter();
                TransactionDescription.DbType = DbType.String;
                TransactionDescription.ParameterName = "@TransactionDescription";
                TransactionDescription.Value = model.TransactionDescription;
                cmd.Parameters.Add(TransactionDescription);

                DbParameter TransactionCode = cmd.CreateParameter();
                TransactionCode.DbType = DbType.String;
                TransactionCode.ParameterName = "@TransactionCode";
                TransactionCode.Value = model.TransactionCode;
                cmd.Parameters.Add(TransactionCode);

                DbParameter TransactionType = cmd.CreateParameter();
                TransactionType.DbType = DbType.Int32;
                TransactionType.ParameterName = "@TransactionType";
                TransactionType.Value = 1;
                cmd.Parameters.Add(TransactionType);

                DbParameter IsVoid = cmd.CreateParameter();
                IsVoid.DbType = DbType.Boolean;
                IsVoid.ParameterName = "@IsVoid";
                IsVoid.Value = false;
                cmd.Parameters.Add(IsVoid);

                DbParameter TransactionStatus = cmd.CreateParameter();
                TransactionStatus.DbType = DbType.String;
                TransactionStatus.ParameterName = "@TransactionStatus";
                TransactionStatus.Value = "Completed";
                cmd.Parameters.Add(TransactionStatus);

                log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.DepositSP));

                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteScalar();

                log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.DepositSP));

                response = UtilityResource.TransactionMessage;

                log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Deposit));
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
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
            string response;

            string requestParameter = "TransactionAmount = " + model.TransactionAmount + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionDescription = " + model.TransactionDescription + ", " + "TransactionCode = " + model.TransactionCode;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.Withdrawal).Replace("{RequestParameter}", requestParameter));

            try
            {
                //check wallet have sufficient balance or not
                log.Append(UtilityResource.CheckBalanceMessage + model.PlayerId);

                ResponseModel bal = GetBalance(model.PlayerId);
                decimal walletBalance = (decimal)bal.Data;
                if (walletBalance >= model.TransactionAmount)
                {
                    this._dbContext.Database.OpenConnection();
                    DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = UtilityResource.WithdrawalSP;

                    DbParameter TransactionAmount = cmd.CreateParameter();
                    TransactionAmount.DbType = DbType.Decimal;
                    TransactionAmount.ParameterName = "@TransactionAmount";
                    TransactionAmount.Value = model.TransactionAmount;
                    cmd.Parameters.Add(TransactionAmount);

                    DbParameter playerID = cmd.CreateParameter();
                    playerID.DbType = DbType.String;
                    playerID.ParameterName = "@PlayerId";
                    playerID.Value = model.PlayerId;
                    cmd.Parameters.Add(playerID);

                    DbParameter TransactionDescription = cmd.CreateParameter();
                    TransactionDescription.DbType = DbType.String;
                    TransactionDescription.ParameterName = "@TransactionDescription";
                    TransactionDescription.Value = model.TransactionDescription;
                    cmd.Parameters.Add(TransactionDescription);

                    DbParameter TransactionCode = cmd.CreateParameter();
                    TransactionCode.DbType = DbType.String;
                    TransactionCode.ParameterName = "@TransactionCode";
                    TransactionCode.Value = model.TransactionCode;
                    cmd.Parameters.Add(TransactionCode);

                    DbParameter TransactionType = cmd.CreateParameter();
                    TransactionType.DbType = DbType.Int32;
                    TransactionType.ParameterName = "@TransactionType";
                    TransactionType.Value = 2;
                    cmd.Parameters.Add(TransactionType);

                    DbParameter IsVoid = cmd.CreateParameter();
                    IsVoid.DbType = DbType.Boolean;
                    IsVoid.ParameterName = "@IsVoid";
                    IsVoid.Value = false;
                    cmd.Parameters.Add(IsVoid);

                    DbParameter TransactionStatus = cmd.CreateParameter();
                    TransactionStatus.DbType = DbType.String;
                    TransactionStatus.ParameterName = "@TransactionStatus";
                    TransactionStatus.Value = "Completed";
                    cmd.Parameters.Add(TransactionStatus);

                    log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.WithdrawalSP));

                    cmd.CommandType = CommandType.StoredProcedure;
                    var reader = cmd.ExecuteScalar();

                    log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.WithdrawalSP));

                    response = UtilityResource.TransactionMessage;
                }
                else
                {
                    log.Append(UtilityResource.InsufficientBalance);

                    response = UtilityResource.InsufficientBalance;
                }

                log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Withdrawal));
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
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
            List<Transaction> list = null;

            string requestParameter = "TransactionFromDate = " + model.TransactionFromDate + ", " + "PlayerId = " + model.PlayerId + ", " + "TransactionToDate = " + model.TransactionToDate;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.TransactionHistory).Replace("{RequestParameter}", requestParameter));

            try
            {
                ResponseModel exist = CheckPlayerExistence(model.PlayerId);

                if (int.Parse(exist.Data.ToString()) != 0)
                {
                    this._dbContext.Database.OpenConnection();
                    DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = UtilityResource.TransactionHistorySP;

                    DbParameter playerID = cmd.CreateParameter();
                    playerID.DbType = DbType.String;
                    playerID.ParameterName = "@PlayerId";
                    playerID.Value = model.PlayerId;
                    cmd.Parameters.Add(playerID);

                    DbParameter TransactionFromDate = cmd.CreateParameter();
                    TransactionFromDate.DbType = DbType.DateTime;
                    TransactionFromDate.ParameterName = "@TransactionFromDate";
                    TransactionFromDate.Value = model.TransactionFromDate;
                    cmd.Parameters.Add(TransactionFromDate);

                    DbParameter TransactionToDate = cmd.CreateParameter();
                    TransactionToDate.DbType = DbType.DateTime;
                    TransactionToDate.ParameterName = "@TransactionToDate";
                    TransactionToDate.Value = model.TransactionToDate;
                    cmd.Parameters.Add(TransactionToDate);

                    log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.TransactionHistorySP));

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = cmd.ExecuteReader())
                    {
                        list = reader.MapToList<Transaction>();
                    }

                    log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.TransactionHistorySP));

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = UtilityResource.SuccessMessage;
                    response.Description = UtilityResource.TrasactionHistoryMessage;
                    response.Data = list;

                    log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.TransactionHistory));
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.PlayerNotExist;
                    response.Data = null;
                    log.Append(UtilityResource.PlayerNotExist);
                }
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
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
            decimal bal = 0;

            string requestParameter = "PlayerId = " + PlayerId;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.GetBalance).Replace("{RequestParameter}", requestParameter));

            try
            {
                ResponseModel exist = CheckPlayerExistence(PlayerId);

                if (int.Parse(exist.Data.ToString()) != 0)
                {
                    this._dbContext.Database.OpenConnection();
                    DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                    cmd.CommandText = UtilityResource.GetBalanceSP;

                    DbParameter playerID = cmd.CreateParameter();
                    playerID.DbType = DbType.String;
                    playerID.ParameterName = "@PlayerId";
                    playerID.Value = PlayerId;
                    cmd.Parameters.Add(playerID);

                    log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.GetBalanceSP));

                    cmd.CommandType = CommandType.StoredProcedure;
                    bal = (decimal)cmd.ExecuteScalar();
                    response.Data = bal;

                    log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.GetBalanceSP));

                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = UtilityResource.SuccessMessage;
                    response.Description = UtilityResource.GetBalanceMessage;

                    log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.GetBalance));
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = UtilityResource.ErrorMessage;
                    response.Description = UtilityResource.PlayerNotExist;
                    response.Data = bal;
                    log.Append(UtilityResource.PlayerNotExist);
                }
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
            }
            return response;
        }

        /// <summary>
        /// cancel a transaction (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string VoidATransaction(TransactionModel model)
        {
            StringBuilder log = new StringBuilder();
            string response;

            string requestParameter = "TransactionId = " + model.TransactionId + ", VoidDescription = " + model.VoidDescription;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.Deposit).Replace("{RequestParameter}", requestParameter));

            try
            {

                this._dbContext.Database.OpenConnection();
                DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = UtilityResource.VoidTransactionSP;

                DbParameter TransactionId = cmd.CreateParameter();
                TransactionId.DbType = DbType.Int64;
                TransactionId.ParameterName = "@TransactionId";
                TransactionId.Value = model.TransactionId;
                cmd.Parameters.Add(TransactionId);

                DbParameter IsVoid = cmd.CreateParameter();
                IsVoid.DbType = DbType.Boolean;
                IsVoid.ParameterName = "@IsVoid";
                IsVoid.Value = true;
                cmd.Parameters.Add(IsVoid);

                DbParameter VoidDescription = cmd.CreateParameter();
                VoidDescription.DbType = DbType.String;
                VoidDescription.ParameterName = "@VoidDescription";
                VoidDescription.Value = model.VoidDescription;
                cmd.Parameters.Add(VoidDescription);

                log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.VoidTransactionSP));

                cmd.CommandType = CommandType.StoredProcedure;
                response = (string)cmd.ExecuteScalar();

                log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.VoidTransactionSP));

                log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.VoidTransaction));

            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
            }

            return response;
        }

        /// <summary>
        /// check, player exist or not (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        public ResponseModel CheckPlayerExistence(string PlayerId)
        {
            StringBuilder log = new StringBuilder();
            ResponseModel response = new ResponseModel();

            string requestParameter = "PlayerId = " + PlayerId;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.CheckPlayerExistence).Replace("{RequestParameter}", requestParameter));

            try
            {
                this._dbContext.Database.OpenConnection();
                DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = UtilityResource.CheckPlayerExistenceSP;

                DbParameter playerID = cmd.CreateParameter();
                playerID.DbType = DbType.String;
                playerID.ParameterName = "@PlayerId";
                playerID.Value = PlayerId;
                cmd.Parameters.Add(playerID);

                log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.CheckPlayerExistenceSP));

                cmd.CommandType = CommandType.StoredProcedure;
                response.Data = (int)cmd.ExecuteScalar();

                log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.CheckPlayerExistenceSP));

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Message = UtilityResource.SuccessMessage;
                response.Description = UtilityResource.SuccessMessage;

                log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.CheckPlayerExistence));
            }
            finally
            {
                LogManagers.LogManagers.WriteTraceLog(log);
                this._dbContext.Database.CloseConnection();
            }
            return response;
        }
    }
}