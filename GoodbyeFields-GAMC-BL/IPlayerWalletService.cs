using GoodbyeFields_GAMC_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_BL
{
    public interface IPlayerWalletService
    {
        /// <summary>
        /// deposit amount in wallet (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Deposit(TransactionModel model);

        /// <summary>
        /// withdrawal some amount frfom wallet (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Withdrawal(TransactionModel model);

        /// <summary>
        /// to fetch the list or transactions for a particular player (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResponseModel TransactionHistory(TransactionModel model);

        /// <summary>
        /// get the balance of a particular player (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        ResponseModel GetBalance(string PlayerId);

        /// <summary>
        /// cancel a transaction (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string VoidATransaction(TransactionModel model);

        /// <summary>
        /// check, player exist or not (Pragati jain 09-01-2020)
        /// </summary>
        /// <param name="PlayerId"></param>
        /// <returns></returns>
        ResponseModel CheckPlayerExistence(string PlayerId);
    }
}
