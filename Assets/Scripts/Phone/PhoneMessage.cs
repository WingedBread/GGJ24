using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhoneMessage
{
    [System.Serializable]
    public enum MessageTypeEnum { File, Big, Medium, Small }
    [System.Serializable]
    public enum MessageSenderEnum { Contact, Reply }
    [System.Serializable]
    public enum MessageContactNameEnum { Maurice, Eli, John, Jessica, Olivia, Beatrice }
    [System.Serializable]
    public enum MessageContactStatusEnum { Online, Away }



    [SerializeField]
    public MessageSenderEnum sender;
    [SerializeField]
    public MessageContactNameEnum contactName;
    [SerializeField]
    public MessageContactStatusEnum contactStatus;
    [SerializeField]
    public Sprite contactPhoto;
    

    [SerializeField]
    public MessageTypeEnum messageType;
    [SerializeField]
    public string messageText;
    [SerializeField]
    public Sprite messageImage;
}
