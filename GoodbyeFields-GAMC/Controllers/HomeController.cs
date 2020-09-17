using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_Model;
using GoodbyeFields_GAMC_Utility;
using GoodbyeFields_GAMC_WalletLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GoodbyeFields_GAMC.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPlayerWalletService _walletService;
        private readonly WalletMethods _wallet;
        private PayPalService _paypalservice;
        private PlayerService _playerservice;

        public HomeController(IPlayerWalletService walletService, PayPalService payPalService)
        {
            _walletService = walletService;
            _wallet = new WalletMethods(_walletService);
            _paypalservice = payPalService;
        }

        /// <summary>
        /// Index Page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Call Deposit Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Deposit()
        {
            return View("Deposite");
        }

        /// <summary>
        /// Call Deposit Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Deposit(TransactionModel model)
        {
            ViewBag.Message = _wallet.Deposit(model);
            return View("Index");
        }

        /// <summary>
        /// Call Void Transaction Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult VoidTransaction()
        {
            return View();
        }

        /// <summary>
        /// Call Void Transaction Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult VoidTransaction(TransactionModel model)
        {
            ViewBag.Message = _wallet.VoidTransaction(model);
            return View("Index");
        }

        /// <summary>
        /// Call Transaction History Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TransactionHistory()
        {
            return View();
        }

        /// <summary>
        /// Call Transaction History Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult TransactionHistory(TransactionModel model)
        {
            List<Transaction> list = new List<Transaction>();
            ResponseModel res = _wallet.TransactionHistory(model);
            ViewBag.Message = res.Description;
            if (res.Description == UtilityResource.PlayerNotExist || res.Description == UtilityResource.PlayerIdError)
            {
                list = null;
            }
            else
            {
                ViewBag.Message = "";
                list = (List<Transaction>)res.Data;
            }
            return View("Ledger", list);
        }

        /// <summary>
        /// Call Transaction Withdrawal Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Withdrawal()
        {
            return View("Withdrawl");
        }

        /// <summary>
        /// Call Transaction Withdrawal Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Withdrawal(TransactionModel model)
        {
            ViewBag.Message = _wallet.Withdrawal(model);
            return View("Index");
        }

        /// <summary>
        /// Call Transaction GetBalance Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBalance()
        {
            return View();
        }

        /// <summary>
        /// Call Transaction GetBalance Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetBalance(TransactionModel model)
        {
            ResponseModel res = _wallet.GetBalance(model.PlayerId);
            if (res.Description == UtilityResource.PlayerNotExist || res.Description == UtilityResource.PlayerIdError)
            {
                ViewBag.Message = res.Description;
            }
            else
            {
                ViewBag.Message = res.Data;
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }

        /// <summary>
        /// Call Payment Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PayAction()
        {
            return View();
        }

        /// <summary>
        /// Call Payment Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PayAction(Transaction model)
        {
            if (ModelState.IsValid)
            {
                Transaction transaction = new Transaction()
                {
                    TransactionAmount = model.TransactionAmount,
                    TransactionDescription = "Service description_" + model.TransactionAmount
                };

                TransactionModel tm = new TransactionModel();
                tm.PlayerId = Guid.NewGuid().ToString();
                tm.TransactionAmount = model.TransactionAmount;
                tm.TransactionDescription = "PayPal payment";
                HttpContext.Session.SetString("trasationModel", JsonConvert.SerializeObject(tm));
                string json = JsonConvert.SerializeObject(transaction);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = httpClient.PostAsync(UtilityResource.HomePayPalAction, content);
                    string apiResponse = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().ToString();
                    transaction.Message = response.Result.RequestMessage.RequestUri.OriginalString;

                }
                return Redirect(transaction.Message);
            }
            return View();
        }

        /// <summary>
        /// Call Success Return
        /// </summary>
        /// <returns></returns>
        [HttpGet]
       public IActionResult SuccesReturn()
        {
            string TransactionID = Request.Query["ID"];
            TransactionModel tm = JsonConvert.DeserializeObject<TransactionModel>(HttpContext.Session.GetString("trasationModel"));
            tm.TransactionCode = TransactionID;
            _wallet.Deposit(tm);
            tm.TransactionDescription = TransactionID;
            return View(tm);
        }

        /// <summary>
        /// Call Cancel Return
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Cancel()
        {      
            return View();
        }
        /// <summary>
        /// Call Failed Return
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Failed()
        {
            string RailureReason = Request.Query["Failure_reason"];
            TransactionModel tm = new TransactionModel();
            tm.TransactionDescription = RailureReason;
            return View(tm);
        }

    }
}
