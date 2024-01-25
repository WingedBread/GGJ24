using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhoneMessage
{
    public enum MessageTypeEnum { File, Big, Medium, Small }
    public enum MessageSenderEnum { Contact, Reply }
    public enum MessageContactNameEnum { Maurice, Eli, John, Jessica, Olivia, Beatrice }
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
