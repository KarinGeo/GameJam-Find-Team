using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IBaseView
    {
        int ViewId { get; set; } //面板Id
        
        Transform ParentTf { get; set; } //所属Canvas

        BaseController Controller { get; set; } //面板所属控制器

        bool IsInit();

        bool IsShow();

        void SetVisible(bool value);

        void Open();

        void Close();

        void Show(params object[] args);

        void Hide();

        void Restore();

        void Init(params object[] args);

    }
}
