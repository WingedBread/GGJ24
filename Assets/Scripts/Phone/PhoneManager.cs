using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public int messageTimerAfterReply;

    [Header("MESSAGE SEQUENCE")]
    public PhoneMessage[] messages;

    public GameObject imageFullScreen;

    private int currentMessage = 0;

    private int starterMessage = 0;

    private bool hasPlayerFinishedMessaging = true;

    public Button replyButton;

    public List<GameObject> allmesages;

    public GameObject backButton;

    //TODO
    //Create message sequencer until player reply

    private void Start()
    {
        audioSource.clip = messageAudioClip;
        replyButton.interactable = false;
        backButton.SetActive(false);
    }
    void Update()
    {
        //For testing porpouses
        if (Input.GetKeyDown(KeyCode.P)) StartMessagingWithTimer();

        if (Input.GetKeyDown(KeyCode.I)) StartNewConversation();

        if (Input.GetKeyDown(KeyCode.O)) ResetPhone();

        if (Input.GetKeyDown(KeyCode.L)) SetAct1State(true);

    }
    public void PlayerReply()
    {
        if (!hasPlayerFinishedMessaging)
        {
            if (currentMessage < messages.Length)
            {
                if (messages[currentMessage].sender == PhoneMessage.MessageSenderEnum.Reply)
                {
                    GameObject message;
                    GameObject container;
                    switch (messages[currentMessage].messageType)
                    {
                        case PhoneMessage.MessageTypeEnum.Small:
                            message = Instantiate(smallMessagePrefab);
                            container = Instantiate(smallMessagePrefab);
                            break;
                        case PhoneMessage.MessageTypeEnum.Medium:
                            message = Instantiate(mediumMessagePrefab);
                            container = Instantiate(mediumMessagePrefab);
                            break;
                        case PhoneMessage.MessageTypeEnum.Big:
                            message = Instantiate(bigMessagePrefab);
                            container = Instantiate(bigMessagePrefab);
                            break;
                        case PhoneMessage.MessageTypeEnum.File:
                            message = Instantiate(fileMessagePrefab);
                            container = Instantiate(fileMessagePrefab);
                            if (messages[currentMessage].messageImage != null)
                            {
                                message.transform.GetChild(1).GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                                imageFullScreen.transform.GetComponent<Image>().sprite = messages[currentMessage].messageImage;
                            }
                            if (message.transform.GetChild(2).GetComponent<Button>().onClick.GetPersistentEventCount() == 0) message.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OpenImageFullScreen());
                            break;
                        default:
                            message = Instantiate(smallMessagePrefab);
                            container = Instantiate(smallMessagePrefab);
                            Debug.LogError("MESSAGE TYPE ERROR");
                            break;
                    }
                    if (messages[currentMessage].messageText != null) message.transform.GetChild(0).GetComponent<TMP_Text>().text = messages[currentMessage].messageText;

                    foreach (Transform child in container.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                    container.transform.GetComponent<Image>().color = new Color(255, 255, 255, 0);

                    container.transform.SetParent(scrollViewContent.transform, false);
                    message.transform.SetParent(scrollViewContent.transform.GetChild(0), false);


                    message.transform.localPosition =
                            new Vector3(355, -allmesages[allmesages.Count - 1].GetComponent<RectTransform>().rect.height + allmesages[allmesages.Count - 1].transform.localPosition.y, 0);

                    allmesages.Add(container);
                    currentMessage++;
                    if (currentMessage < messages.Length && messages[currentMessage].sender != PhoneMessage.MessageSenderEnum.Reply)
                    {
                        replyButton.interactable = false;
                        if(contactName.text == Enum.GetName(typeof(PhoneMessage.MessageContactNameEnum), messages[currentMessage].contactName))
                        {
                            InvokeRepeating("MessagesBehaviour", messageTimerAfterReply, messageTimer);
                            Debug.Log("Contact Reply");
                        }
                    }
                    if(currentMessage >= messages.Length)
                    {
                        hasPlayerFinishedMessaging = true;
                        replyButton.interactable = false;
                    }
                    ForceScrollViewToBottom();
                }
            }

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
        if (!hasPlayerFinishedMessaging)
        {
            if (currentMessage < messages.Length)
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

                    if (currentMessage == starterMessage)
                    {
                        message.transform.localPosition = new Vector3(22, -45, 0);
                        audioSource.Play();
                    }
                    else
                    {
                        message.transform.localPosition =
                            new Vector3(22, -allmesages[allmesages.Count - 1].GetComponent<RectTransform>().rect.height + allmesages[allmesages.Count - 1].transform.localPosition.y - 30, 0);
                    }

                    allmesages.Add(message);

                    currentMessage++;
                    ForceScrollViewToBottom();
                    if (currentMessage >= messages.Length)
                    {
                        hasPlayerFinishedMessaging = true;
                        replyButton.interactable = false;
                    }
                }
                else
                {
                    CancelInvoke("MessagesBehaviour");
                    Debug.Log("Canceled Messages");
                    replyButton.interactable = true;
                }
            }
            else
            {
                hasPlayerFinishedMessaging = true;
                replyButton.interactable = false;
            }
        }
    }

    public void ResetPhone()
    {
        Debug.Log("Phone Reseted");
        foreach (GameObject go in allmesages)
        {
            Destroy(go);
        }
        hasPlayerFinishedMessaging = true;
        replyButton.interactable = false;
        starterMessage = currentMessage;
    }

    public void StartMessagingWithTimer()
    {
        if (!IsInvoking("MessagesBehaviour") && currentMessage < messages.Length && !hasPlayerFinishedMessaging &&  contactName.text == Enum.GetName(typeof(PhoneMessage.MessageContactNameEnum), messages[currentMessage].contactName))
        {
            InvokeRepeating("MessagesBehaviour", 0, messageTimer);
            Debug.Log("Messages Start");
        }
        
    }
    public void StartNewConversation()
    {
        if (!IsInvoking("MessagesBehaviour") && currentMessage < messages.Length && hasPlayerFinishedMessaging)
        {
            hasPlayerFinishedMessaging = false;
            MessagesBehaviour();
        }
    }

    public void ForceScrollViewToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void SetAct1State(bool hasFinished)
    {
        if (hasFinished)
        {
            backButton.SetActive(true);
        }
        else
        {
            backButton.SetActive(false);
        }
    }
}
