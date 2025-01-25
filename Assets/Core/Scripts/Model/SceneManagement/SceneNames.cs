using System;

public static class SceneNames
{
    public const string Boot = "Boot";
    public const string LoadingScene = "LoadingScene";
    public const string Ui = "Ui";
    public const string MainMenu = "MainMenu";
    public const string GamePlay = "GamePlay";

    public static string GetSceneName(SceneTypes sceneType)
    {
        Type type = sceneType.GetType();
        var memberInfo = type.GetMember(sceneType.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(SceneNameAttribute), false);

        return ((SceneNameAttribute)attributes[0]).Name;
    }
}

public enum SceneTypes
{
    [SceneName(SceneNames.Boot)] Boot = -99,
    [SceneName(SceneNames.LoadingScene)] LoadingScene,
    [SceneName(SceneNames.Ui)] Ui,
    [SceneName(SceneNames.MainMenu)] MainMenu,
    [SceneName(SceneNames.GamePlay)] GamePlay,
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class SceneNameAttribute : Attribute
{
    public string Name { get; }

    public SceneNameAttribute(string name)
    {
        Name = name;
    }
}