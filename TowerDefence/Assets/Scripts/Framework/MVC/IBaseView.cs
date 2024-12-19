using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IBaseView
    {
        int ViewId { get; set; } //���Id
        
        Transform ParentTf { get; set; } //����Canvas

        BaseController Controller { get; set; } //�������������

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
