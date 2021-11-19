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

    // PLACEHOLDER SCORE
    private int loveScore = 0;

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
        Debug.Log(chapters.Count);
        Debug.Log(chapterIndex);

        Debug.Log(chapters[chapterIndex].Count());
        Debug.Log(storyIndex);

        if (chapterIndex + 1 == chapters.Count && storyIndex + 1 == chapters[chapterIndex].Count())
        {
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

        Debug.Log(_currentRoom);
        for (int i = 0; i < chapters.Count; i++)
        {
            if (_currentRoom + 1 < chapters[i].Count())
            {
                storyIndex = _currentRoom + 1;
                chapterIndex = i;
                Debug.Log(storyIndex);
                Debug.Log(chapterIndex);
                diaMan.SetCurrentStory(chapters[chapterIndex].GetStory(storyIndex));
            }
            else
            {
                _currentRoom -= chapters[i].Count();
            }
        }
    }
}
