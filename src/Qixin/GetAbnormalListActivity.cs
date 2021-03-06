﻿using System;
using System.Activities;
using System.ComponentModel;
using System.Drawing;
using Newtonsoft.Json;
using Qixin.ApiService;
using Qixin.Designers;

namespace Qixin
{
    [DisplayName("企业经营异常查询")]
    [ToolboxBitmap(typeof(GetAbnormalListActivity), "Assets.qixin.png")]
    [Designer(typeof(GetAbnormalListActivityDesigner))]
    public sealed class GetAbnormalListActivity : BaseQixinActivity
    {
        public GetAbnormalListActivity()
        {
            DisplayName = "企业经营异常查询";
            Skip = new InArgument<int>(0);
        }

        [RequiredArgument]
        [Category("输入")]
        [DisplayName("企业全名/注册号/统一社会信用代码")]
        public InArgument<string> Name { get; set; }

        [Category("输入")]
        [DisplayName("跳过条目数")]
        public InArgument<int> Skip { get; set; }

        [Category("输出")]
        [DisplayName("经营异常列表")]
        public OutArgument<string> Content { get; set; }

        [Category("输出")] [DisplayName("记录总数")] public OutArgument<int> Total { get; set; }

        [Category("输出")]
        [DisplayName("返回记录数")]
        public OutArgument<int> Num { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            var keyVal = AppKey.Get(context);
            var nameVal = Name.Get(context);
            var skipVal = Skip.Get(context);
            var apiServiceBase = new GetAbnormalListService(keyVal, skipVal);
            var actual = apiServiceBase.GetAbnormalList(nameVal);

            if (actual.Status == 200)
            {
                Content.Set(context, actual.Items != null ? JsonConvert.SerializeObject(actual.Items) : string.Empty);
                Total.Set(context, actual.Total);
                Num.Set(context, actual.Num);
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