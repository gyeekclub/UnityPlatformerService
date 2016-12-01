using Microsoft.Azure.Mobile.Server;

namespace UnityPlatformerService.DataObjects
{
    public class Player : EntityData
    {
        public string Name { get; set; }
        public int Coins { get; set; }
    }
}
