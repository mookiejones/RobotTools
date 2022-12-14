using System;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using miRobotEditor.Core.Classes;
using miRobotEditor.Core.Classes.Messaging;
using miRobotEditor.Core.Handlers;

namespace miRobotEditor.ViewModels
{
    public class MessageViewModel:ViewModelBase
   {
       #region Members
       private const string ToolContentId = @"MessageViewTool";
       public event MessageAddedHandler MessageAdded;

       #endregion

       static BitmapImage GetMsgIcon(MsgIcon icon)
       {

           var result = icon == MsgIcon.Error ? Global.ImgError : Global.ImgInfo;
           var image = Utilities.LoadBitmap(result);

           return image;
       }

       void RaiseMessageAdded()
       {
           if (MessageAdded != null)
               MessageAdded(this, new EventArgs());
       }


       #region Constructor

       public MessageViewModel() : base()
       {
           
       }


       #endregion
   }
}
