using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using Newtonsoft.Json;
using Qixin.ApiService;
using Qixin.Designers;

namespace Qixin
{
    [DisplayName("企业工商数据查询")]                                     // 当前组件在编辑器的工具栏显示的名称
    [ToolboxBitmap(typeof(GetAbnormalListActivity), "Assets.qixin.png")]  // 组件在工具栏上的图标
    [Designer(typeof(GetAbnormalListActivityDesigner))]                   // 当前组件在编辑器上的UI
    public sealed class GetBusinessSearchActivity : BaseQixinActivity     // 组件的具体类实现
    {
        public GetBusinessSearchActivity()
        {
            DisplayName = "企业工商数据查询";               // 组件拖动到编辑器里的时候显示的名称
        }

        [RequiredArgument]                                  // 当前参数为用户必填参数
        [Category("输入")]                                  // 当前参数属于“输入”类别
        [DisplayName("企业全名/注册号/统一社会信用代码")]   // 当前参数在编辑器上显示的名称
        public InArgument<string> Name { get; set; }        // 输入参数

        [Category("输出")] [DisplayName("工商数据")]
        public OutArgument<string> Content { get; set; }    // 输出参数

        protected override void Execute(NativeActivityContext context)
        {
            var keyVal = AppKey.Get(context);
            var nameVal = Name.Get(context);
            var apiServiceBase = new GetBusinessSearchService(keyVal);
            var actual = apiServiceBase.GetBusinessList(nameVal);

            if (actual.Status == 200)
            {
                // 如果获取结果成果，则将结果设置到输出参数
                Content.Set(context, actual.Items != null ? JsonConvert.SerializeObject(actual.Items) : string.Empty);
            }
            else if (actual.Status == 201)
            {
                Console.WriteLine(actual.Message);
            }
            else
            {
                throw new ArgumentException(actual.Message);
            }
        }
    }
}