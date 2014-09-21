using BulletinBoard.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
                
        [Inject]
        protected IRepository repository { get; set; }
        //
        // GET: /Home/

        public BaseController()
        {
            repository = DependencyResolver.Current.GetService<IRepository>();
        }

        protected delegate bool Rule();
        protected bool CheckRuleAndSetErrMessage(Rule rule, string key, string errMessage)
        {
            if (rule())
            {
                return true;
            }
            else
            {
                ModelState.AddModelError(key, errMessage);
                return false;
            }
        }
    }
}
