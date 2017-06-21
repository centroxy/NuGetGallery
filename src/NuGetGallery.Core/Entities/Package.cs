﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NuGetGallery
{
    [DisplayColumn("Title")]
    public class Package
        : IEntity
    {

#pragma warning disable 618 // TODO: remove Package.Authors completely once production services definitely no longer need it
        public Package()
        {
            Authors = new HashSet<PackageAuthor>();
            Dependencies = new HashSet<PackageDependency>();
            PackageEdits = new HashSet<PackageEdit>();
            PackageHistories = new HashSet<PackageHistory>();
            PackageTypes = new HashSet<PackageType>();
            SupportedFrameworks = new HashSet<PackageFramework>();
            Listed = true;
        }
#pragma warning restore 618

        public PackageRegistration PackageRegistration { get; set; }
        public int PackageRegistrationKey { get; set; }

        [Obsolete("Will be removed in a future iteration, for now is write-only")]
        public virtual ICollection<PackageAuthor> Authors { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string Copyright { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }

        public virtual ICollection<PackageDependency> Dependencies { get; set; }

        public virtual ICollection<PackageType> PackageTypes { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed but *IS* used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string Description { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string ReleaseNotes { get; set; }

        public int DownloadCount { get; set; }

        /// <remarks>
        ///     Is not a property that we support. Maintained for legacy reasons.
        /// </remarks>
        [Obsolete]
        public string ExternalPackageUrl { get; set; }

        [StringLength(10)]
        public string HashAlgorithm { get; set; }

        /// <summary>
        /// Stringified hashcode generated by hashing the nupkg file with HashAlgorithm.
        /// </summary>
        [StringLength(256)]
        [Required]
        public string Hash { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string IconUrl { get; set; }

        public bool IsLatest { get; set; }
        public bool IsLatestStable { get; set; }

        public bool IsLatestSemVer2 { get; set; }
        public bool IsLatestStableSemVer2 { get; set; }

        /// <summary>
        /// This is when the Package Entity was last touched (so caches can notice changes). In UTC.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// This is when the Package Metadata was last edited by a user. Or NULL. In UTC.
        /// 
        /// This field is updated by a trigger on the database if it is edited.
        /// This trigger is defined by a migration named "AddTriggerForPackagesLastEdited".
        /// The trigger guarantees that the timestamps of multiple instances of the gallery do not conflict.
        /// </summary>
        public DateTime? LastEdited { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string LicenseUrl { get; set; }

        public bool HideLicenseReport { get; set; }

        [StringLength(20)]
        public string Language { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Published { get; set; }

        /// <summary>The size of the nupkg file in bytes.</summary>
        public long PackageFileSize { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string ProjectUrl { get; set; }

        public string RepoUrl { get; set; }

        public bool RequiresLicenseAcceptance { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and not used for searches. Db column is nvarchar(max).
        /// </remarks>
        public string Summary { get; set; }

        /// <remarks>
        ///     Has a max length of 4000. Is not indexed and *IS* used for searches, but is maintained via Lucene. Db column is nvarchar(max).
        /// </remarks>
        public string Tags { get; set; }

        [StringLength(256)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the version listed in the manifest for this package, which MAY NOT conform to NuGet's use of SemVer
        /// </summary>
        [StringLength(64)]
        [Required]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the version for this package that has been normalized to conform to NuGet's use of SemVer
        /// </summary>
        [StringLength(64)]
        public string NormalizedVersion { get; set; }

        /// <summary>
        /// Gets or sets the minimum required SemVer level for consumers of this package, 
        /// based on the Version property containing the original version string,
        /// or the dependency version ranges of this package.
        /// </summary>
        /// <remarks>
        /// If the field value is null, the SemVer level of this version is unknown,
        /// and could either indicate the package version is SemVer1- or non-SemVer-compliant at all (e.g. System.Versioning pattern).
        /// </remarks>
        public int? SemVerLevelKey { get; set; }

        public virtual ICollection<PackageLicenseReport> LicenseReports { get; set; }

        // Pre-calculated data for the feed
        public string LicenseNames { get; set; }
        public string LicenseReportUrl { get; set; }

        public bool Listed { get; set; }
        public bool IsPrerelease { get; set; }
        public virtual ICollection<PackageFramework> SupportedFrameworks { get; set; }

        public string FlattenedAuthors { get; set; }

        public string FlattenedDependencies { get; set; }

        public string FlattenedPackageTypes { get; set; }

        public int Key { get; set; }

        [StringLength(44)]
        public string MinClientVersion { get; set; }

        /// <summary>
        /// The logged in user when this package version was created.
        /// NULL for older packages.
        /// </summary>
        public User User { get; set; }
        public int? UserKey { get; set; }

        /// <summary>
        /// The set of pending edits to package metadata (for this package).
        /// Asynchronously updated by the worker.
        /// </summary>
        public virtual ICollection<PackageEdit> PackageEdits { get; set; }

        /// <summary>
        /// List of historical metadata info of this package (before edits were applied)
        /// </summary>
        public virtual ICollection<PackageHistory> PackageHistories { get; set; }

        public bool Deleted { get; set; }

        public void ApplyEdit(PackageEdit edit, string hashAlgorithm, string hash, long packageFileSize)
        {
            // before we modify this package, record its state in history
            PackageHistories.Add(new PackageHistory(this));

            Title = edit.Title;
            FlattenedAuthors = edit.Authors;
            Copyright = edit.Copyright;
            Description = edit.Description;
            IconUrl = edit.IconUrl;
            LicenseUrl = edit.LicenseUrl;
            ProjectUrl = edit.ProjectUrl;
            ReleaseNotes = edit.ReleaseNotes;
            RequiresLicenseAcceptance = edit.RequiresLicenseAcceptance;
            Summary = edit.Summary;
            Tags = edit.Tags;
            User = edit.User;

            Hash = hash;
            HashAlgorithm = hashAlgorithm;
            PackageFileSize = packageFileSize;

            var now = DateTime.UtcNow;
            LastUpdated = now;
            LastEdited = now;
        }
    }
}
