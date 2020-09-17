using GoodbyeFields_GAMC_Model;
using System.Collections.Generic;

namespace GoodbyeFields_GAMC_BL
{
    public interface IPlayerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Player> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Player GetById(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SavePayPalResponse(PayPalResponseModel model);
    }
}
