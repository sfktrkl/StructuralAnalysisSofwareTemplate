using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StructuralAnalysisSofwareTemplate
{
    public static class Database
    {
        public static readonly Dictionary<string, Component> NodeList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> MemberList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> MaterialList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> SectionList = new Dictionary<string, Component>();
    }
}