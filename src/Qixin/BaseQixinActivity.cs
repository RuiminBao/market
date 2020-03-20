using System.Activities;
using System.ComponentModel;

namespace Qixin
{
    [Category("启信宝")]
    public abstract class BaseQixinActivity : NativeActivity
    {
        [Category("输入")]
        [RequiredArgument]
        [DisplayName("AppKey")]
        public InArgument<string> AppKey { get; set; }
    }
}