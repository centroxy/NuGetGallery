﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NuGetGallery
{
    /// <summary>
    /// This is a pending package metadata edit, where a user has decided to redescribe a package and have the nupkg regenerated.
    /// </summary>
    public class PackageEdit : IEntity
    {
        public int Key { get; set; }

        public Package Package { get; set; }
        public int PackageKey { get; set; }

        /// <summary>
        /// User who edited the package and thereby caused this Metadata to be created.
        /// </summary>
        public User User { get; set; }
        public int UserKey { get; set; }

        /// <summary>
        /// Time this metadata was generated
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Count so that the worker role can tell itself not to retry processing this edit forever if it gets stuck.
        /// </summary>
        public int TriedCount { get; set; }

        #region Rest = same as Package

        public string Authors { get; set; }
        public string Copyright { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string LicenseUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string ReleaseNotes { get; set; }
        public bool RequiresLicenseAcceptance { get; set; }
        public string Summary { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }

        #endregion
    }
}