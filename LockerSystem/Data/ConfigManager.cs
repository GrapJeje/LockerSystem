using System;
using System.IO;
using System.Text.Json;

namespace LockerSystem.Data
{
    public abstract class ConfigManager
    {
        private object? _config;
        private string? _fileName;
        private string? _subFolder;

        protected abstract Type ConfigType { get; }

        public void Reload()
        {
            var configFile = GetConfigFilePath();

            if (!File.Exists(configFile))
            {
                SaveDefaultConfig();
            }

            try
            {
                var json = File.ReadAllText(configFile);
                _config = JsonSerializer.Deserialize(json, ConfigType)
                    ?? throw new InvalidOperationException("Failed to deserialize JSON file.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to reload configuration from {configFile}: {ex.Message}");
            }
        }

        public object GetConfig()
        {
            if (_config == null)
            {
                Reload();
            }
            return _config!;
        }

        public bool Save()
        {
            var configFile = GetConfigFilePath();
            try
            {
                var json = JsonSerializer.Serialize(_config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(configFile, json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Could not save configuration to {configFile}: {ex.Message}");
                return false;
            }

            Reload();
            return true;
        }

        public void SaveDefaultConfig()
        {
            var configFile = GetConfigFilePath();

            if (!File.Exists(configFile))
            {
                var defaultConfig = Activator.CreateInstance(ConfigType)
                    ?? throw new InvalidOperationException("Failed to create default config instance.");

                var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Directory.CreateDirectory(Path.GetDirectoryName(configFile)!);
                File.WriteAllText(configFile, json);
            }
        }

        public string GetConfigFilePath()
        {
            var folder = GetFolder();
            var fileName = GetFileName();

            Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }

        public string GetFolder()
        {
            return Path.Combine(AppContext.BaseDirectory, SubFolder ?? "");
        }

        public string GetFileName()
        {
            return FileName ?? throw new InvalidOperationException("FileName is not set.");
        }

        public string? SubFolder
        {
            get => _subFolder;
            set => _subFolder = value;
        }

        public string? FileName
        {
            get => _fileName;
            set => _fileName = value;
        }
    }
}
