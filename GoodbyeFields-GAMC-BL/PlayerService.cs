using GoodbyeFields_GAMC_DataLayer;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GoodbyeFields_GAMC_BL
{
    public class PlayerService : IPlayerService
    {
       
        private readonly AppSettings _appSettings;
        private readonly GoodbyeFieldsGAMCDBContext _dbContext;

       
        /// <param name="appSettings"></param>
        /// <param name="dbContext"></param>
        public PlayerService(IOptions<AppSettings> appSettings, GoodbyeFieldsGAMCDBContext dbContext)
        {
            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get Player List
        /// </summary>
        /// <returns></returns>
       public List<Player> getList()
        {
           List<Player> _player = null;
            try
            {
                this._dbContext.Database.OpenConnection();
                DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = UtilityResource.PlayerList;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    _player = reader.MapToList<Player>();
                }
                return _player;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this._dbContext.Database.CloseConnection();
            }
        }
        
        /// <summary>
        /// Call Authenticate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            StringBuilder log = new StringBuilder();
            log.Append("Enter into Authenticate method");
            var user = getList().SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null) return null;
            var token = generateJwtToken(user);
            log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.Authenticate));
            return new AuthenticateResponse(user, token);
        }

        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string generateJwtToken(Player user)
        {
            StringBuilder log = new StringBuilder();
            log.Append("Enter into Authenticate method");

            // generate token that is valid for 5 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("PlayerId", user.PlayerId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            log.Append(UtilityResource.ExecutedSuccessfully.Replace("{MethodName}", UtilityResource.GenerateJwtToken));
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Call GetAll
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> GetAll()
        {
            return getList(); //_users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Player GetById(string id)
        {
            return getList().FirstOrDefault(x => x.PlayerId == id);
        }

        /// <summary>
        /// Save PayPal Response in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SavePayPalResponse(PayPalResponseModel model)
        {
            StringBuilder log = new StringBuilder();
            string response = string.Empty;

            string requestParameter = "Id = " + model.Id + ", " + "CreateTime = " + model.CreateTime + ", " + "UpdateTime = " + model.UpdateTime + ", " + "Intent = " + model.Intent + ", " + 
                "PaymentMethod = " + model.PaymentMethod + ", " + "PayerId = " + model.PayerId + ", " + "Amount = " + model.Amount + ", " + "Currency = " + model.Currency + ", " + 
                "Description = " + model.Description + ", " + "Status = " + model.Status + "FullResponse = " + model.FullResponse;
            log.Append(UtilityResource.LogServiceStartMessage.Replace("{MethodName}", UtilityResource.SavePaypalResonseService).Replace("{RequestParameter}", requestParameter));

            try
            {
                this._dbContext.Database.OpenConnection();
                DbCommand cmd = this._dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = UtilityResource.SavePaypalResonseSP;


                DbParameter Id = cmd.CreateParameter();
                Id.DbType = DbType.String;
                Id.ParameterName = "@Id";
                Id.Value = model.Id;
                cmd.Parameters.Add(Id);

                DbParameter CreateTime = cmd.CreateParameter();
                CreateTime.DbType = DbType.DateTime;
                CreateTime.ParameterName = "@CreateTime";
                CreateTime.Value = model.CreateTime;
                cmd.Parameters.Add(CreateTime);

                DbParameter UpdateTime = cmd.CreateParameter();
                UpdateTime.DbType = DbType.DateTime;
                UpdateTime.ParameterName = "@UpdateTime";
                UpdateTime.Value = model.UpdateTime;
                cmd.Parameters.Add(UpdateTime);

                DbParameter Intent = cmd.CreateParameter();
                Intent.DbType = DbType.String;
                Intent.ParameterName = "@Intent";
                Intent.Value = model.Intent;
                cmd.Parameters.Add(Intent);

                DbParameter PaymentMethod = cmd.CreateParameter();
                PaymentMethod.DbType = DbType.String;
                PaymentMethod.ParameterName = "@PaymentMethod";
                PaymentMethod.Value = model.PaymentMethod;
                cmd.Parameters.Add(PaymentMethod);

                DbParameter PayerId = cmd.CreateParameter();
                PayerId.DbType = DbType.String;
                PayerId.ParameterName = "@PayerId";
                PayerId.Value = model.PayerId;
                cmd.Parameters.Add(PayerId);

                DbParameter Amount = cmd.CreateParameter();
                Amount.DbType = DbType.Decimal;
                Amount.ParameterName = "@Amount";
                Amount.Value = model.Amount;
                cmd.Parameters.Add(Amount);

                DbParameter Currency = cmd.CreateParameter();
                Currency.DbType = DbType.String;
                Currency.ParameterName = "@Currency";
                Currency.Value = model.Currency;
                cmd.Parameters.Add(Currency);

                DbParameter Description = cmd.CreateParameter();
                Description.DbType = DbType.String;
                Description.ParameterName = "@Description";                   
                Description.Value = model.Description;
                cmd.Parameters.Add(Description);

                DbParameter Status = cmd.CreateParameter();
                Status.DbType = DbType.String;
                Status.ParameterName = "@Status";
                Status.Value = model.Status;
                cmd.Parameters.Add(Status);

                DbParameter InvoiceNumber = cmd.CreateParameter();
                InvoiceNumber.DbType = DbType.String;
                InvoiceNumber.ParameterName = "@InvoiceNumber";
                InvoiceNumber.Value = model.InvoiceNumber;
                cmd.Parameters.Add(InvoiceNumber);

                DbParameter FullResponse = cmd.CreateParameter();
                FullResponse.DbType = DbType.String;
                FullResponse.ParameterName = "@FullResponse";
                FullResponse.Value = model.FullResponse;
                cmd.Parameters.Add(FullResponse);

                log.Append(UtilityResource.SPExecutionStart.Replace("{SPName}", UtilityResource.SavePaypalResonseSP));

                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteScalar();

                log.Append(UtilityResource.SPExecutionEnd.Replace("{SPName}", UtilityResource.SavePaypalResonseSP));

                response = UtilityResource.TransactionMessage;

                log.Append(UtilityResource.ServiceExecutedSuccessfully.Replace("{MethodName}", UtilityResource.SavePaypalResonseService));
            }
            catch (Exception ex)
            {
                log.Append(UtilityResource.ErrorInMethod.Replace("{MethodName}", UtilityResource.SavePayPalResponse).Replace("{ErrorMessage}", ex.Message));
                LogManagers.LogManagers.WriteErrorLog(ex);
                response = ex.Message;
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
