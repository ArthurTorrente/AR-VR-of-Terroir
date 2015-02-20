/*==============================================================================
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 *==============================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SC_UserDefinedTargetEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
{
    /// <summary>
    /// Can be set in the Unity inspector to reference a ImageTargetBehaviour that is instanciated for augmentations of new user defined targets.
    /// </summary>
    public ImageTargetBehaviour ImageTargetTemplate;
    

    private UserDefinedTargetBuildingBehaviour mTargetBuildingBehaviour;
    private ImageTracker mImageTracker;
    // DataSet that newly defined targets are added to
    private DataSet mBuiltDataSet;
    // currently observed frame quality
    private ImageTargetBuilder.FrameQuality mFrameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;
    // counter variable used to name duplicates of the image target template
    private int mTargetCounter;



    /// <summary>
    /// Registers this component as a handler for UserDefinedTargetBuildingBehaviour events
    /// </summary>
    public void Start()
    {
        mTargetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
        if (mTargetBuildingBehaviour)
        {
            mTargetBuildingBehaviour.RegisterEventHandler(this);
            Debug.Log ("Registering to the events of IUserDefinedTargetEventHandler");
        }
    }

    /// <summary>
    /// Called when UserDefinedTargetBuildingBehaviour has been initialized successfully
    /// </summary>
    public void OnInitialized ()
    {
        mImageTracker = TrackerManager.Instance.GetTracker<ImageTracker>();
        if (mImageTracker != null)
        {
            // create a new dataset
            mBuiltDataSet = mImageTracker.CreateDataSet();
            mImageTracker.ActivateDataSet(mBuiltDataSet);
        }
    }

    /// <summary>
    /// Updates the current frame quality
    /// </summary>
    public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
    {
        mFrameQuality = frameQuality;
    }

    /// <summary>
    /// Takes a new trackable source and adds it to the dataset
    /// This gets called automatically as soon as you 'BuildNewTarget with UserDefinedTargetBuildingBehaviour
    /// </summary>
    public void OnNewTrackableSource(TrackableSource trackableSource)
    {
        mTargetCounter++;
        
        // deactivates the dataset first
        mImageTracker.DeactivateDataSet(mBuiltDataSet);
        
        // Destroy the oldest target if the dataset is full or the dataset 
        // already contains five user-defined targets.
        if (mBuiltDataSet.HasReachedTrackableLimit() || mBuiltDataSet.GetTrackables().Count() >= 5)
        {
            IEnumerable<Trackable> trackables = mBuiltDataSet.GetTrackables();
            Trackable oldest = null;
            foreach (Trackable trackable in trackables)
                if (oldest == null || trackable.ID < oldest.ID)
                    oldest = trackable;
            
            if (oldest != null)
            {
                Debug.Log("Destroying oldest trackable in UDT dataset: " + oldest.Name);
                mBuiltDataSet.Destroy(oldest, true);
            }
        }
        
        // get predefined trackable and instantiate it
        ImageTargetBehaviour imageTargetCopy = (ImageTargetBehaviour)Instantiate(ImageTargetTemplate);
        imageTargetCopy.gameObject.name = "UserDefinedTarget-" + mTargetCounter;
        
        // add the duplicated trackable to the data set and activate it
        mBuiltDataSet.CreateTrackable(trackableSource, imageTargetCopy.gameObject);
        
        
        // activate the dataset again
        mImageTracker.ActivateDataSet(mBuiltDataSet);

    }
 

    /// <summary>
    /// Instantiates a new user-defined target and is also responsible for dispatching callback to 
    /// IUserDefinedTargetEventHandler::OnNewTrackableSource
    /// </summary>
    public void BuildNewTarget()
    {
        // create the name of the next target.
        // the TrackableName of the original, linked ImageTargetBehaviour is extended with a continuous number to ensure unique names
        string targetName = string.Format("{0}-{1}", ImageTargetTemplate.TrackableName, mTargetCounter);
        
        // generate a new target name:
        
        mTargetBuildingBehaviour.BuildNewTarget(targetName, ImageTargetTemplate.GetSize().x);
    }
}



