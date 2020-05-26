using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kavenegar;
using Kavenegar.Core.Models;
using Kavenegar.Core.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace TestDotNetPakages.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            //خط ارسال کننده
            string[] sender = { "10008663", "10008663", "10008663", "10008663", "10008663" };
            
            //لیست شماره گیرندگان پیام
            string[] receptor = { "09112345678", "09112345678", "", "09112345678", "09012345678" };

            //لیست پیام های ارسالی
            string[] message = { "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار", "تست وب سرویس کاوه نگار" };

            //شناسه پیام هایی که مثلا در دیتابیس لوکال قرار دارند

            string[] localIDs = { new Random().Next(0, 2454).ToString(), new Random().Next(0, 12544).ToString(),
            new Random().Next(0, 45645).ToString(),new Random().Next(0, 2000000).ToString(),
            new Random().Next(0, 123123456).ToString(),};

            //Your Api Key شناسه شما در پنل کاوه نگار
            KavenegarApi kavenegar = new KavenegarApi("Your Api Key");

            SendResult result = null;
            List<SendResult> resultList = null;

            StatusResult statusResult = null;
            List<StatusResult> statusResultList = null;

            StatusLocalMessageIdResult StatusLocalMessageIdResultResult = null;
            List<StatusLocalMessageIdResult> StatusLocalMessageIdResultResultList = null;

            CountInboxResult CountInboxResult = null;


            #region SelectAsync
            result = await kavenegar.Select("274037533");
            resultList = await kavenegar.Select(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region SelectOutboxAsync
            resultList = await kavenegar.SelectOutbox(DateTime.Now.AddDays(-1), DateTime.Now);
            resultList = await kavenegar.SelectOutbox(DateTime.Now.AddDays(-2));
            #endregion

            #region SendByPostalCodeAsync 
            resultList = await kavenegar.SendByPostalCode(4451865169, sender[0], "slama", 0, 10, 0, 16);
            resultList = await kavenegar.SendByPostalCode(4451865169, sender[0], "slama", 0, 10, 0, 16, DateTime.Now);
            #endregion

            #region StatusAsync 
            statusResult = await kavenegar.Status("1775698101");
            statusResultList = await kavenegar.Status(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region StatusLocalMessageIdAsync 
            StatusLocalMessageIdResultResult = await kavenegar.StatusLocalMessageId(localIDs[0]);
            StatusLocalMessageIdResultResultList = await kavenegar.StatusLocalMessageId(localIDs.ToList());
            #endregion

            #region CancelAsync 
            statusResult = await kavenegar.Cancel("1775698101");
            statusResultList = await kavenegar.Cancel(new List<string>() { "1775698101", "1775696560" });
            #endregion

            #region CountInboxAsync 
            CountInboxResult = await kavenegar.CountInbox(DateTime.Now.AddDays(-1), sender[0]);
            CountInboxResult = await kavenegar.CountInbox(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1), sender[0]);
            #endregion

            #region CountOutboxAsync 
            CountInboxResult = await kavenegar.CountOutbox(DateTime.Now.AddDays(-1));
            CountInboxResult = await kavenegar.CountOutbox(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1));
            #endregion

            #region CountPostalCodeAsync 
            List<CountPostalCodeResult> countPostalCodeResult = await kavenegar.CountPostalCode(4451865169);
            #endregion

            #region LatestOutboxAsync 
            resultList = await kavenegar.LatestOutbox(1);
            resultList = await kavenegar.LatestOutbox(1, sender[0]);
            #endregion

            #region LatestOutboxAsync 
            List<ReceiveResult> receiveResult = await kavenegar.Receive(sender[0], 0);
            List<ReceiveResult> ReceiveResult = await kavenegar.Receive(sender[0], 1);
            #endregion

            #region sendAsync
            result = await kavenegar.Send(sender[0], receptor[0], message[0]);
            result = await kavenegar.Send(sender[0], receptor[0], message[0], localIDs[0].ToString());
            #endregion

            #region sendArrayAsync
            resultList = await kavenegar.SendArray(sender.ToList(), receptor.ToList(), message.ToList(), localIDs[0]);
            resultList = await kavenegar.SendArray(sender[0], receptor.ToList(), message.ToList(), localIDs[0]);
            #endregion

            #region VerifyLookupAsync
            result = await kavenegar.VerifyLookup(receptor[0], "123", "verify");
            result = await kavenegar.VerifyLookup(receptor[0], "123", null, null, null, "token20", "rate", VerifyLookupType.Sms);
            #endregion

            return View();
        }
    }
}
