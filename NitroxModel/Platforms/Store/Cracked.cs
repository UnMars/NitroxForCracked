using System;
using System.IO;
using System.Threading.Tasks;
using NitroxModel.Discovery.Models;
using NitroxModel.Helper;
using NitroxModel.Platforms.OS.Shared;
using NitroxModel.Platforms.Store.Interfaces;

namespace NitroxModel.Platforms.Store {

    public sealed class Cracked : IGamePlatform {


        private static Cracked instance;
        public static Cracked Instance => instance ??= new Cracked();

        public string Name => "Cracked";
        public Platform Platform => Platform.STEAM;

        public bool OwnsGame(string gameDirectory)
        {
            string steamDll = Path.Combine(gameDirectory, "steam_api64.dll");
            return File.Exists(steamDll) && new FileInfo(steamDll).Length >= 209000;
        }

        public async Task<ProcessEx> StartPlatformAsync()
        {
            await Task.CompletedTask; // Suppresses async-without-await warning - can be removed.
            throw new NotImplementedException(); // Not necessary to implement unless EGS gets a game SDK and respective game integrates it.
        }

        public string GetExeFile()
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessEx> StartGameAsync(string pathToGameExe, string launchArguments)
        {
            // Normally should call StartPlatformAsync first. But Subnautica will start without EGS.
            return await Task.FromResult(
                ProcessEx.Start(
                    pathToGameExe,
                    new[] { (NitroxUser.LAUNCHER_PATH_ENV_KEY, NitroxUser.LauncherPath) },
                    Path.GetDirectoryName(pathToGameExe),"")
                );
        }
    }
}
