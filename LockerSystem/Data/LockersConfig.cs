using System;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LockerSystem.Data
{
    public class LockersConfig : ConfigManager
    {
        public LockersConfig() => FileName = "lockers.json";

        protected override Type ConfigType => typeof(LockersConfig);

        private JsonObject GetLocker(int number)
        {
            var lockersJson = File.ReadAllText(FileName);
            var lockers = JsonSerializer.Deserialize<JsonArray>(lockersJson);
            var locker = lockers?.FirstOrDefault(l => l["Number"]?.GetValue<int>() == number);

            return locker?.AsObject();
        }

        public bool GetOccupied(int number)
        {
            var locker = GetLocker(number);

            if (locker == null) return true;

            return locker["IsOccupied"]?.GetValue<bool>() ?? false;
        }

        public int? GetPassword(int number)
        {
            var locker = GetLocker(number);

            if (locker == null) return null;

            return locker["Password"]?.GetValue<int>() ?? null;
        }

        public void SetLocker(int number, bool occupied, int? password)
        {
            var locker = GetLocker(number);
            if (locker == null) return;

            locker["Password"] = password;
            locker["IsOccupied"] = occupied;
            Save();
        }
    }
}
