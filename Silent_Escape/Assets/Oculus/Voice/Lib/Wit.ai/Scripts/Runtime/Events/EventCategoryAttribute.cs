/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using UnityEngine;

namespace Facebook.WitAi.Events
{
    public class EventCategoryAttribute : PropertyAttribute
    {
        public readonly string Category;

        public EventCategoryAttribute(string category = "")
        {
            Category = category;
        }
    }
}
