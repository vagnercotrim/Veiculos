using System.Collections.Generic;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Veiculos.Infra.Notice;

namespace System.Web.Mvc
{
    public static class NoticeHelper
    {

        public static IList<Notice> GetNoticesFromTempData(this ControllerBase controller)
        {
            var notices = controller.TempData["notices"];

            if (notices == null)
                return new List<Notice>();

            return (List<Notice>)notices;
        }

        public static void ClearNotices(this ControllerBase controller)
        {
            controller.TempData["notices"] = new List<Notice>();
        }
        
        public static void AddNotice(this ControllerBase controller, IList<Notice> erros)
        {
            controller.TempData["notices"] = erros;
        }

        public static void AddNotice(this ControllerBase controller, string mensagem, NoticeType classe)
        {
            try
            {
                IList<Notice> notices = GetNoticesFromTempData(controller);
                notices.Add(new Notice(mensagem, classe));
                AddNotice(controller, notices);
            }
            catch { }
        }

        public static void Success(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Success);
        }

        public static void Info(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Info);
        }

        public static void Warning(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Warning);
        }

        public static void Error(this ControllerBase controller, string mensagem)
        {
            AddNotice(controller, mensagem, NoticeType.Error);
        }

    }
}