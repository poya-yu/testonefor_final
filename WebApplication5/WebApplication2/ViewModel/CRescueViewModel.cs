using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class CRescueViewModel
    {
        public tRescue Rescue { get; set; }
        public HttpPostedFileBase RescuePictures { get; set; }
        public CRescueViewModel()
        { 
            this.Rescue = new tRescue();
        }

        public int RescueID { get { return this.Rescue.RescueID; } set { this.Rescue.RescueID = value; } }
        public int RescueMemberID { get { return this.Rescue.RescueMemberID; } set { this.Rescue.RescueMemberID = value; } }
        public string RescueTitle { get { return this.Rescue.RescueTitle; } set { this.Rescue.RescueTitle = value; } }
        public string ResCueDone { get { return this.Rescue.ResCueDone; } set { this.Rescue.ResCueDone = value; } }
        //public string RescuePictures { get { return this.Rescue.RescuePictures; } set { this.Rescue.RescuePictures = value; } }
        public int RescuePosition { get { return this.Rescue.RescuePosition; } set { this.Rescue.RescuePosition = value; } }
        public int RescuePosition1 { get { return this.Rescue.RescuePosition1; } set { this.Rescue.RescuePosition1 = value; } }
        public string RescueEvent { get { return this.Rescue.RescueEvent; } set { this.Rescue.RescueEvent = value; } }
        public int RescueSpecies { get { return this.Rescue.RescueSpecies; } set { this.Rescue.RescueSpecies = value; } }
        public string RescueProgress { get { return this.Rescue.RescueProgress; } set { this.Rescue.RescueProgress = value; } }
        public System.DateTime Created_At { get { return this.Rescue.Created_At; } set { this.Rescue.Created_At = value; } }




        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowRescue> FollowRescue { get; set; }
        public virtual tMember tMember { get; set; }
        public virtual tPosition tPosition { get; set; }
        public virtual tPosition tPosition1 { get; set; }
        public virtual tSpecies tSpecies { get; set; }


    }
}