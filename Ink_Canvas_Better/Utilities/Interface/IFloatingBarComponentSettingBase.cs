namespace Ink_Canvas_Better.Utilities.Interface;

/// <summary>
/// All floating bar component settings must implement this interface to be recognized by the application.
/// </summary>
public interface IFloatingBarComponentSettingBase
{
    /// <summary>
    /// Settings of the component.
    /// </summary>
    public object Settings { get; set; }

    /// <summary>
    /// Provides a way to invoke it without knowing its type. Return true if invoke successfully.
    /// </summary>
    /// <remarks>
    /// <b>Please return <c>true</c> instead of throwing NotImplementedException when the control has nothing to do.</b>
    /// </remarks>
    /// <returns>
    /// <c>true</c> if invoke successfully<br/>
    /// <c>false</c> if happens something wrong<br/>
    /// </returns>
    public bool TryInvoke();
}
