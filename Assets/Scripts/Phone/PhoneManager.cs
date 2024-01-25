using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine.Rendering;
using System.Linq;

public class PhoneManager : MonoBehaviour
{

    [SerializeField]
    public GameObject scrollViewContent;
    [SerializeField]
    public ScrollRect scrollRect;

    [Header("MESSAGE PREFABS CONTACT")]
    public GameObject bigMessagePrefab;
    public GameObject mediumMessagePrefab;
    public GameObject smallMessagePrefab;
    public GameObject fileMessagePrefab;

    [Header("CONTACT INFO")]
    public TMP_Text contactName;
    public TMP_Text contactStatus;
    public Image contactPhoto;

    [Header("AUDIO")]
    public AudioSource audioSource;
    public AudioClip messageAudioClip;

    [Header("TIMER")]
    public int messageTimer;

    [Header("MESSAGE SEQUENCE")]
    public PhoneMessage[] messages;

    public GameObject imageFullScreen;

    private int currentMessage = 0;
    

    private bool hasPlayerFinishedMessaging = false;

    public Button replyButton;

    public List<GameObject> allmesages;

    //TODO
    //Create message sequencer until player reply

    private void Start()
    {
        audioSource.clip = messageAudioClip;
    }
    void Update()
    {
        //For testing porpouses
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!hasPlayerFinishedMessaging) MessagesBehaviour();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (hasPlayerFinishedMessaging) PhoneClosed();
        }
    }
    public void PlayerReply()
    {
        if (messages[currentMessage].sender == PhoneMessage.MessageSenderEnum.Reply)
        {
            GameObject message;
            switch (messages[currentMessage].messageType)
            {
                case PhoneMessage.MessageTypeEnum.Small:
                    message = Instantiate(smallMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.Medium:
                    message = Instantiate(mediumMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.Big:
                    message = Instantiate(bigMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.File:
                    message = Instantiate(fileMessagePrefab);
                    if (messages[currentMessage].messageImage != null)
                    {
                        message.transform.GetChild(1).GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                        imageFullScreen.transform.GetChild(0).GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                    }
                    if (message.transform.GetChild(2).GetComponent<Button>().onClick.GetPersistentEventCount() == 0) message.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OpenImageFullScreen());
                    break;
                default:
                    message = Instantiate(smallMessagePrefab);
                    Debug.LogError("MESSAGE TYPE ERROR");
                    break;
            }
            if (messages[currentMessage].messageText != null) message.transform.GetChild(0).GetComponent<TMP_Text>().text = messages[currentMessage].messageText;

            message.transform.SetParent(scrollViewContent.transform, false);

            message.transform.localPosition =
                    new Vector3(385, -allmesages[allmesages.Count - 1].GetComponent<RectTransform>().rect.height + allmesages[allmesages.Count - 1].transform.localPosition.y - 30, 0);

            allmesages.Add(message);
            hasPlayerFinishedMessaging = true;
            currentMessage++;
            replyButton.interactable = false;
        }
    }

    public void OpenImageFullScreen()
    {
        imageFullScreen.transform.SetSiblingIndex(imageFullScreen.transform.parent.childCount);
        imageFullScreen.SetActive(true);
        scrollRect.vertical = false;
    }

    public void CloseImageFullScreen()
    {
        imageFullScreen.SetActive(false);
        scrollRect.vertical = true;
    }

    public void MessagesBehaviour()
    {
        if (messages[currentMessage].sender == PhoneMessage.MessageSenderEnum.Contact)
        {
            contactName.text = Enum.GetName(typeof(PhoneMessage.MessageContactNameEnum), messages[currentMessage].contactName);
            contactStatus.text = Enum.GetName(typeof(PhoneMessage.MessageContactStatusEnum), messages[currentMessage].contactStatus);
            if (messages[currentMessage].contactPhoto != null) contactPhoto.sprite = messages[currentMessage].contactPhoto;

            GameObject message;
            switch (messages[currentMessage].messageType)
            {
                case PhoneMessage.MessageTypeEnum.Small:
                    message = Instantiate(smallMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.Medium:
                    message = Instantiate(mediumMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.Big:
                    message = Instantiate(bigMessagePrefab);
                    break;
                case PhoneMessage.MessageTypeEnum.File:
                    message = Instantiate(fileMessagePrefab);
                    if (messages[currentMessage].messageImage != null)
                    {
                        message.transform.GetChild(1).GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                        imageFullScreen.transform.GetChild(0).GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                    }
                    if (message.transform.GetChild(2).GetComponent<Button>().onClick.GetPersistentEventCount() == 0) message.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OpenImageFullScreen());
                    break;
                default:
                    message = Instantiate(smallMessagePrefab);
                    Debug.LogError("MESSAGE TYPE ERROR");
                    break;
            }
            if (messages[currentMessage].messageText != null) message.transform.GetChild(0).GetComponent<TMP_Text>().text = messages[currentMessage].messageText;

            message.transform.SetParent(scrollViewContent.transform, false);

            if (currentMessage == 0 || currentMessage == 4) message.transform.localPosition = new Vector3(22, -45, 0);
            else
            {
                message.transform.localPosition =
                    new Vector3(22, -allmesages[allmesages.Count - 1].GetComponent<RectTransform>().rect.height + allmesages[allmesages.Count - 1].transform.localPosition.y - 30, 0);
            }

            allmesages.Add(message);
            
            audioSource.Play(); 

            currentMessage++;
        }
    }

    public void PhoneClosed()
    {
        foreach (GameObject go in allmesages)
        {
            Destroy(go);
        }
        hasPlayerFinishedMessaging = false;
        replyButton.interactable = true;
    }
}
