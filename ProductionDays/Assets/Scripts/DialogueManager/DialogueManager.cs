using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ExpressionChanger expChange;
    [SerializeField] private UIControllerDialogue UICont;

    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;

    private bool dialogueStarted = false;

    static Story story;
    TMP_Text nametagHelvetia;
    TMP_Text nametagOther;
    TMP_Text message;
    List<string> tags;
    static Choice choiceSelected;

    private int buttonSize = 50;
    private bool showChoices = false;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        nametagHelvetia = textBox.transform.GetChild(0).GetComponent<TMP_Text>();
        nametagOther = textBox.transform.GetChild(1).GetComponent<TMP_Text>();
        message = textBox.transform.GetChild(2).GetComponent<TMP_Text>();
        message.text = "";
        tags = new List<string>();
        choiceSelected = null;
    }

    private void Update()
    {
        
    }

    void OnConfirmButton()
    {
        if (!optionPanel.activeInHierarchy && dialogueStarted)
        {
            //Are there any choices?
            if (story.currentChoices.Count != 0)
            {
                StartCoroutine(ShowChoices());
            }

            //Is there more to the story?
            if (story.canContinue || optionPanel.activeInHierarchy)
            {
                //nametag.text = "Phoenix";
                if (!optionPanel.activeInHierarchy && !showChoices)
                {
                    AdvanceDialogue();
                }
            }
            else
            {
                FinishDialogue();
            }
        }   
    }

    // Finished the Story (Dialogue)
    private void FinishDialogue()
    {
        Debug.Log("End of Dialogue!");
        dialogueStarted = false;
        UICont.ScaleDown();
    }

    // Advance through the story 
    void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        ParseTags();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    // Type out the sentence letter by letter and make character idle if they were talking
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
        yield return null;
    }

    // Create then show the choices on the screen until one got selected
    IEnumerator ShowChoices()
    {
        nametagHelvetia.gameObject.SetActive(true);
        nametagOther.gameObject.SetActive(false);
        message.gameObject.SetActive(false);

        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TMP_Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });

            temp.GetComponent<RectTransform>().localPosition -= new Vector3(0, i*buttonSize, 0);
        }
        
        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    // Tells the story which branch to go to
    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    // After a choice was made, turn off the panel and advance from that choice
    void AdvanceFromDecision()
    {
        message.gameObject.SetActive(true);
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        AdvanceDialogue();
        AdvanceDialogue();
    }

    /*** Tag Parser ***/
    /// In Inky, you can use tags which can be used to cue stuff in a game.
    /// This is just one way of doing it. Not the only method on how to trigger events. 
    void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string[] subs = t.Split(' ');

            if (subs[0].Equals("Helvetia"))
            {
                nametagHelvetia.gameObject.SetActive(true);
                nametagOther.gameObject.SetActive(false);
            }
            else
            {
                nametagHelvetia.gameObject.SetActive(false);
                nametagOther.gameObject.SetActive(true);
                nametagOther.text = subs[0];
            }
            
            if(subs.Length > 1)
            {
                // change sprite according to emotion
                if (subs[0].Equals("Helvetia"))
                {
                    expChange.ChangeExpressionLeft(subs[1]);
                }
                else
                {
                    expChange.ChangeExpressionRight(subs[1]);
                }
                

            }
        }
    }
    /*
    void SetTextColor(string _color)
    {
        switch (_color)
        {
            case "red":
                message.color = Color.red;
                break;
            case "blue":
                message.color = Color.cyan;
                break;
            case "green":
                message.color = Color.green;
                break;
            case "white":
                message.color = Color.white;
                break;
            default:
                Debug.Log($"{_color} is not available as a text color");
                break;
        }
    }*/

    public void StartDialogue()
    {
        dialogueStarted = true;
        UICont.ScaleUp();
    }
}