using DiscordBotCore.Handler;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace BotService
{
    public partial class BotService : ServiceBase
    {
        public BotService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();
            ChannelHandler s = new ChannelHandler();
            Task.Run(async () => {
                await s.RunBotAsync();
            })
            .ConfigureAwait(false);
            
        }

        protected override void OnStop()
        {
        }
    }
}
