using System;
using System.Collections.Generic;
using Bee.Core;
using Bee.Stevedore;
using Bee.Stevedore.Program;
using Unity.BuildSystem.NativeProgramSupport;

namespace BuildProgram
{
	public class BuildProgram
	{
		private static readonly Dictionary<string, Tuple<string, string>> Artifacts = new Dictionary<string, Tuple<string, string>>();

		internal static void Main()
		{
			RegisterCommonArtifacts();

			if (Platform.HostPlatform is WindowsPlatform)
			{
				RegisterWindowsArtifacts();
			}
			else
			{
				RegisterCommonNonWindowsArtifacts();

				if (Platform.HostPlatform is MacOSXPlatform)
				{
					RegisterOSXArtifacts();
				}
				else if (Platform.HostPlatform is LinuxPlatform)
				{
					RegisterLinuxArtifacts();
				}
			}

			foreach (var artifact in Artifacts)
			{
				var name = artifact.Key;
				var id = artifact.Value.Item1;
				var repo = new RepoName(artifact.Value.Item2);

				Console.WriteLine($">>> Registering artifact {name}");
				var stevedoreArtifact = new StevedoreArtifact(repo, new ArtifactId(id));
				Backend.Current.Register(stevedoreArtifact);
			}
		}

		private static void RegisterCommonArtifacts()
		{
			Artifacts.Add("MonoBleedingEdge",
				new Tuple<string, string>(
					"MonoBleedingEdge/fd0d97a7a35_5d627f842afebea942027a7fe8a590effb76deaf44736482b8bbcfae58316d42.7z",
					"unity-internal"));

			Artifacts.Add("reference-assemblies",
				new Tuple<string, string>(
					"reference-assemblies/1.0_fc1889ab066ec621a44e51c666d750590b0496d8284b4420e1119c26ce0c7462.7z",
					"unity-internal"));
		}

		private static void RegisterWindowsArtifacts()
		{
			Artifacts.Add("android-ndk-windows-x86_64",
				new Tuple<string, string>(
					"android-ndk-windows-x86_64/r16b_4c6b39939b29dfd05e27c97caf588f26b611f89fe95aad1c987278bd1267b562.7z",
					"unity-internal"));
		}

		private static void RegisterOSXArtifacts()
		{
			Artifacts.Add("android-ndk-darwin-x86_64",
				new Tuple<string, string>(
					"android-ndk-darwin-x86_64/r16b_9654a692ed97713e35154bfcacb0028fdc368128d636326f9644ed83eec5d88b.7z",
					"unity-internal"));

			Artifacts.Add("MacBuildEnvironment",
				new Tuple<string, string>(
					"MacBuildEnvironment/9df1e3b3b120_2fc8e616a2e5dfb7907fc42d9576b427e692223c266dc3bc305de4bf03714e30.7z",
					"unity-internal"));

			Artifacts.Add("mono-build-tools-extra",
				new Tuple<string, string>(
					"mono-build-tools-extra/9de3c42ef81ec4f79b53e7db32d390227d8c43c4_fa9931c37b7a4ca636eb9e0e48252c4cb591caaa9b77c41b75795037868c1256.7z",
					"unity-internal"));
		}

		private static void RegisterLinuxArtifacts()
		{
			Artifacts.Add("android-ndk-linux-x86_64",
				new Tuple<string, string>(
					"android-ndk-linux-x86_64/r16b_bcdea4f5353773b2ffa85b5a9a2ae35544ce88ec5b507301d8cf6a76b765d901.7z",
					"unity-internal"));

			Artifacts.Add("linux-sdk-20170609",
				new Tuple<string, string>(
					"linux-sdk-20170609/9df1e3b3b120_9a3a0847d5b3767579e908b5a9ce050936617b1b9275a79a8b71bb3229998957.7z",
					"unity-internal"));
		}

		private static void RegisterCommonNonWindowsArtifacts()
		{
			Artifacts.Add("libtool-src",
				new Tuple<string, string>(
					"libtool-src/2.4.6_49a0ed204b3b24572e044400cd05513f611bcca6ced0d0816a57ac3b17376257.7z",
					"public"));

			Artifacts.Add("texinfo-src",
				new Tuple<string, string>(
					"texinfo-src/4.8_975b9657ebef8a4fe3897047ca450b757a0a956b05399dc813f63e84829bac6a.7z",
					"public"));

			Artifacts.Add("automake-src",
				new Tuple<string, string>(
					"automake-src/1.16.1_d281b950e26265f55f0a63188a8c6388e638b354b7ed80d186690119cbc4f953.7z",
					"public"));

			Artifacts.Add("autoconf-src",
				new Tuple<string, string>(
					"autoconf-src/2.69_0e4ba7a0363c68ad08a7d138b228596aecdaea68e1d8b8eefc645e6ac8fc85c7.7z",
					"public"));

			Artifacts.Add("libgdiplus-mac",
				new Tuple<string, string>(
					"libgdiplus-mac/9df1e3b3b120_4cf7c08770db93922f54f38d2461b9122cddc898db58585864446e70c5ad3057.7z",
					"unity-internal"));
		}
	}
}