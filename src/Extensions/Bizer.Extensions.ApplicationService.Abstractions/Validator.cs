using System.ComponentModel.DataAnnotations;

namespace Bizer.Extensions.ApplicationService.Abstractions;

/// <summary>
/// 模型对象验证器。
/// </summary>
public class Validator
{
    /// <summary>
    /// 尝试使用 <c>System.ComponentModel.DataAnnotations</c> 的方式验证指定对象并输出验证失败的结果。
    /// </summary>
    /// <param name="model">要验证的对象。</param>
    /// <param name="errors">返回的验证错误集合。</param>
    /// <returns>验证成功，则返回 <c>true</c>；否则返回 <c>false</c>。</returns>
    public static bool TryValidate(object model, out IEnumerable<string> errors)
    {
        var validationResult = new List<ValidationResult>();
        if (System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, new ValidationContext(model), validationResult))
        {
            errors = Array.Empty<string>();
            return true;
        }

        errors = validationResult.ConvertAll(x => x.ErrorMessage ?? string.Empty);

        return false;
    }
}