using System;

namespace XFlow.Core.Framework.CustomProperties.XFlow
{
    [Serializable]
    public class XFormItem
    {
        public Guid? FormId { get; set; }
        public string FormName { get; set; }
        
        public static XFormItem Empty
        {
            get
            {
                return new XFormItem
                {
                    FormId = null,
                    FormName = string.Empty
                };
            }
        }
    }
}
//"/secure/UI/CMS/edit/XFormSelect.aspx?form=d883ef9b-88de-43e2-83ed-dd3d08e50215&pageId=36_37&parentId=3";