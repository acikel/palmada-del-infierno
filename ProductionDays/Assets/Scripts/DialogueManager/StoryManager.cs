using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

[System.Serializable]
public class Chapter
{
    public List<TextAsset> stories = new List<TextAsset>();
    

    public int Count()
    {
        return stories.Count;
    }

    public TextAsset GetStory(int _storyIndex)
    {
        return stories[_storyIndex];
    }
}

public class StoryManager : MonoBehaviour
{

    [SerializeField] private List<Chapter> chapters = new List<Chapter>();
    private int numberOfStories;

    public int chapterIndex = 0;
    private int storyIndex = 0;

    private DialogueManager diaMan;

    public bool storyComplete = false;

    [SerializeField] private int lastLinearChapter = 2;
    [SerializeField] private int BJDoboChapter = 2;
    [SerializeField] private int BhomasTuchelinChapter = 3;

    private bool lastStoryLoaded= false;

    // PLACEHOLDER SCORE
    public int loveScore = 0;

    void Awake()
    {
        for(int i = 0; i < chapters.Count; i++)
        {
            numberOfStories += chapters[i].Count();
        }

        diaMan = GetComponent<DialogueManager>();
    }

    public TextAsset NextStory()
    {
        /*
        Debug.Log(chapters.Count);
        Debug.Log(chapterIndex);

        Debug.Log(chapters[chapterIndex].Count());
        Debug.Log(storyIndex);
        */

        /*
        if (lastStoryLoaded)
        {
            storyComplete = true;
            chapterIndex--;
        }*/

        if ((chapterIndex + 2 == chapters.Count || chapterIndex + 1 == chapters.Count) && storyIndex == 2)
        {
            //lastStoryLoaded = true;
            storyComplete = true;
            chapterIndex--;
        }

        if(storyIndex + 1 < chapters[chapterIndex].Count())
        {
            storyIndex++;
        }
        else
        {
            storyIndex = 0;
            if(chapterIndex < lastLinearChapter)
            {
                chapterIndex++;
            }
            else
            {
                // all linear chapters finished
                // get chapter according to score
                if(loveScore < 0)
                {
                    chapterIndex = BJDoboChapter;
                }
                else
                {
                    chapterIndex = BhomasTuchelinChapter;
                }
            }
        }
        return GetCurrentStory();
    }

    public TextAsset GetCurrentStory()
    {
        return chapters[chapterIndex].GetStory(storyIndex);
    }

    public void UpdateLoveMeter(object _value)
    {
        loveScore += (int)_value;
    }

    public void SetCurrentStory(int _currentRoom)
    {
        //int _nmbrOfStories = numberOfStories;
        // load room+1 story

        for (int i = 0; i < chapters.Count; i++)
        {
            Debug.Log("Room" + _currentRoom);
            Debug.Log("stories" + chapters[i].Count());
            if (_currentRoom + 1 < chapters[i].Count())
            {
                storyIndex = _currentRoom + 1;
                chapterIndex = i;
                Debug.Log("Story" + storyIndex);
                Debug.Log("chapter" + chapterIndex);
                if (LevelManager.Instance.mainMenuEntered)
                {
                    storyIndex = 0;
                    chapterIndex = 0;
                }

                diaMan.SetCurrentStory(chapters[chapterIndex].GetStory(storyIndex));
                return;
            }
            else
            {
                _currentRoom -= chapters[i].Count();
            }
        }
    }
}
