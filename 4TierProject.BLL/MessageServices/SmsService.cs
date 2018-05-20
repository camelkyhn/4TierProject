using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace _4TierProject.BLL.MessageServices
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Metin iletisi göndermek için SMS hizmetinizi buraya bağlayın.
            return Task.FromResult(0);
        }
    }
}
