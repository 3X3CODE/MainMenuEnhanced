using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx;
using Il2CppInterop.Runtime;

namespace MainPlugin.Assets;

// this assetloader code was made by Gemini since i was too lazy
public static class AssetLoader
{
    // Path.Combine handles the slashes correctly for the user's OS
    // This points to: BepInEx/plugins/CustomBG.png
    private static readonly string ImagePathPNG = Path.Combine(Paths.PluginPath, "CustomBG.png");
    private static readonly string ImagePathJPG = Path.Combine(Paths.PluginPath, "CustomBG.jpg");
    private static readonly string ImagePathJPEG = Path.Combine(Paths.PluginPath, "CustomBG.jpeg");
    private static string ImagePath;

    public static Sprite LoadExternalSprite()
    {
        // 1. Check if the file actually exists to prevent a crash
        if (!File.Exists(ImagePathPNG) && !File.Exists(ImagePathJPG) && !File.Exists(ImagePathJPEG))
        {
            MainMenuEnhancedPlugin.LogSource.LogWarning($"[Signal] External image not found at {ImagePathPNG}. Using default background.");
            return null;
        }

        if (File.Exists(ImagePathPNG)) ImagePath = ImagePathPNG;
        if (File.Exists(ImagePathJPG)) ImagePath = ImagePathJPG;
        if (File.Exists(ImagePathJPEG)) ImagePath = ImagePathJPEG;

        try
        {
            // 2. Read the raw bytes from the disk
            byte[] fileData = File.ReadAllBytes(ImagePath);

            // 3. Create a texture placeholder (size will be auto-adjusted by LoadImage)
            Texture2D texture = new Texture2D(2, 2);

            // 4. Load the bytes into the texture
            // This requires the UnityEngine.ImageConversionModule reference
            if (ImageConversion.LoadImage(texture, fileData))
            {
                // 5. Turn the texture into a Sprite
                // Pivot (0.5f, 0.5f) centers the image
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        catch (Exception e)
        {
            MainMenuEnhancedPlugin.LogSource.LogError($"[Signal] Failed to load external sprite: {e.Message}");
        }

        return null;
    }

    public static Sprite LoadSprite(string path)
    {
        if (!File.Exists(path))
        {
            MainMenuEnhancedPlugin.LogSource.LogInfo("sprite doesn't exist");
        }
        try
        {
            // 2. Read the raw bytes from the disk
            byte[] fileData = File.ReadAllBytes(path);

            // 3. Create a texture placeholder (size will be auto-adjusted by LoadImage)
            Texture2D texture = new Texture2D(2, 2);

            // 4. Load the bytes into the texture
            // This requires the UnityEngine.ImageConversionModule reference
            if (ImageConversion.LoadImage(texture, fileData))
            {
                // 5. Turn the texture into a Sprite
                // Pivot (0.5f, 0.5f) centers the image
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
        catch (Exception e)
        {
            MainMenuEnhancedPlugin.LogSource.LogError($"[Signal] Failed to load sprite: {e.Message}");
        }

        return null;
    
    }

    public static GameObject LoadAsset(string bundleName, string assetName)
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        string resourceName = asm.GetManifestResourceNames()
            .FirstOrDefault(name => name.Contains(bundleName));

        if (resourceName == null)
        {
            MainMenuEnhancedPlugin.LogSource.LogError($"[Mod] Could not find {bundleName} in DLL");
            return null;
        }

        using (Stream s = asm.GetManifestResourceStream(resourceName))
        {
            byte[] buffer = new byte[s.Length];
            s.Read(buffer, 0, buffer.Length);

            AssetBundle bundle = AssetBundle.LoadFromMemory(buffer);

            if (bundle == null)
            {
                MainMenuEnhancedPlugin.LogSource.LogError($"[Mod] Failed to load bundle from memory");
                return null;
            }

            var asset = bundle.LoadAsset(assetName, Il2CppInterop.Runtime.Il2CppType.Of<GameObject>());
            GameObject prefab = asset.Cast<GameObject>();
            bundle.Unload(false);
            return prefab;
        }
    }
}
