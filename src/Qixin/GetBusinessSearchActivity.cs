using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using Newtonsoft.Json;
using Qixin.ApiService;
using Qixin.Designers;

namespace Qixin
{
    [DisplayName("企业工商数据查询")]
    [ToolboxBitmap(typeof(GetAbnormalListActivity), "Assets.qixin.png")]
    [Designer(typeof(GetAbnormalListActivityDesigner))]
    public sealed class GetBusinessSearchActivity : BaseQixinActivity
    {
        public GetBusinessSearchActivity()
        {
            DisplayName = "企业工商数据查询";
        }

        [RequiredArgument]
        [Category("输入")]
        [DisplayName("企业全名/注册号/统一社会信用代码")]
        public InArgument<string> Name { get; set; }

        [Category("输出")] [DisplayName("工商数据")] public OutArgument<string> Content { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            var keyVal = AppKey.Get(context);
            var nameVal = Name.Get(context);
            var apiServiceBase = new GetBusinessSearchService(keyVal);
            var actual = apiServiceBase.GetBusinessList(nameVal);

            if (actual.Status == 200)
                Content.Set(context, actual.Items != null ? JsonConvert.SerializeObject(actual.Items) : string.Empty);
            else if (actual.Status == 201)
                Console.WriteLine(actual.Message);
            else
                throw new ArgumentException(actual.Message);
        }
    }
}