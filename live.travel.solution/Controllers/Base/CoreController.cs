using live.travel.solution.Models.Core;
using Microsoft.AspNetCore.Mvc;

namespace live.travel.solution.Controllers.Base {
    public class CoreController : Controller {

        /// <summary>
        /// set tempdata messages
        /// </summary>
        /// <param name="msg">Write your message</param>
        /// <param name="msgType">Define your message type</param>
        protected void SetMessage(string msg, MsgType msgType) {
            switch (msgType) {
                case MsgType.Success:
                    TempData["Message"] = msg;
                    break;
                case MsgType.Error:
                    TempData["Error"] = msg;
                    break;
                case MsgType.Info:
                    TempData["Info"] = msg;
                    break;
            }
        }
    }
}
