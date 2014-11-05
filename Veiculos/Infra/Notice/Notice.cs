using System;

namespace Veiculos.Infra.Notice
{
    public class Notice
    {

        public String Title { get; private set; }

        public String Message { get; private set; }

        public NoticeType Type { get; private set; }

        public bool IsSticky { get; private set; }

        public Notice(String title, String message, NoticeType type, bool isSticky)
        {
            IsSticky = isSticky;
            Type = type;
            Message = message;
            Title = title;
        }

        public Notice(String title, String message, NoticeType type) : this(title, message, type, false)
        {

        }

        public Notice(String message, NoticeType type) : this(null, message, type, false)
        {

        }

    }
}