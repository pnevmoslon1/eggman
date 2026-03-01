using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamContractModel.Extensions;

public static class StringExtensions
{
    public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);

    public static bool IsGuid(this string str) => Guid.TryParse(str, out _);
}
