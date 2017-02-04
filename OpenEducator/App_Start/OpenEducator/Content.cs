﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OpenEducator {

    public abstract class Content: ICanJsonSerialized {

        public string ContentType {
            get {
                return GetType().FullName;
            }
        }

        public string[] Classes { get; set; } = new string[] { };
        public string ID { get; set; } = "";
        public string Style { get; set; } = "";
        /// <summary>
        /// Put directly inside tag
        /// </summary>
        public string Open { get; set; } = "";

        /// <summary>
        /// Wrap data into a tag with optional properties.
        /// </summary>
        /// <param name="domElement">Tag (div, p, etc.)</param>
        /// <param name="data">InnerHTML</param>
        /// <param name="style">Inline Style</param>
        /// <param name="classes">List of Classes</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static string Wrap(string domElement, string data, string style = "", string[] classes = null, string id = null, string open = null) {
            return $@"<{domElement}" +
                ((classes == null || classes.Length == 0) ? "" : (" class=\"" + string.Join(" ", classes) + "\"")) +
                (string.IsNullOrWhiteSpace(id) ? "" : (" id=\"" + id + "\"")) +
                (string.IsNullOrWhiteSpace(style) ? "" : (" style=\"" + style + "\"")) +
                (string.IsNullOrWhiteSpace(open) ? "" : $" {open}") +
                $@">{data}</{domElement}>";
        }

        /// <summary>
        /// Create a self-closing tag with optional properties.
        /// </summary>
        /// <param name="domElement">Tag (div, p, etc.)</param>
        /// <param name="style">Inline Style</param>
        /// <param name="classes">List of Classes</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static string WrapSelfClosing(string domElement, string style = null, string[] classes = null, string id = null, string open = null) {
            return $@"<{domElement}" +
                ((classes == null || classes.Length == 0) ? "" : (" class=\"" + string.Join(" ", classes) + "\"")) +
                (string.IsNullOrWhiteSpace(id) ? "" : (" id=\"" + id + "\"")) +
                (string.IsNullOrWhiteSpace(style) ? "" : (" style=\"" + style + "\"")) +
                (string.IsNullOrWhiteSpace(open) ? "" : $"{open}") +
                $@"/{domElement}>";
        }

        public abstract string Render();

        public string JsonString() {
            return JsonConvert.SerializeObject(this);
        }

    }
}