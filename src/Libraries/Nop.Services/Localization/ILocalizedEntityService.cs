﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Localization;

namespace Nop.Services.Localization
{
    /// <summary>
    /// Localized entity service interface
    /// </summary>
    public partial interface ILocalizedEntityService
    {
        /// <summary>
        /// Find localized properties
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the found localized properties
        /// </returns>
        Task<IList<LocalizedProperty>> GetEntityLocalizedPropertiesAsync(int entityId, string localeKeyGroup, string localeKey);

        /// <summary>
        /// Find localized value
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="localeKeyGroup">Locale key group</param>
        /// <param name="localeKey">Locale key</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the found localized value
        /// </returns>
        Task<string> GetLocalizedValueAsync(int languageId, int entityId, string localeKeyGroup, string localeKey);

        /// <summary>
        /// 지정된 번역된 이름으로 ProductAttribute가 있는지 확인합니다.
        /// </summary>
        /// <param name="localeKeyGroup">Locale key group.</param>
        /// <param name="localeKey">Locale key.</param>
        /// <param name="localizedName">확인하려는 번역된 이름.</param>
        /// <returns>
        /// 일치하는 ProductAttribute가 있으면 true를 반환하고, 없으면 false를 반환합니다.
        /// </returns>
        public Task<int?> GetEntityIdByLocalizedNameAsync(string localeKeyGroup, string localeKey, string localizedName);

        /// <summary>
        /// Save localized value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="localeValue">Locale value</param>
        /// <param name="languageId">Language ID</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task SaveLocalizedValueAsync<T>(T entity,
            Expression<Func<T, string>> keySelector,
            string localeValue,
            int languageId) where T : BaseEntity, ILocalizedEntity;

        /// <summary>
        /// Save localized value
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <typeparam name="TPropType">Property type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="keySelector">Key selector</param>
        /// <param name="localeValue">Locale value</param>
        /// <param name="languageId">Language ID</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task SaveLocalizedValueAsync<T, TPropType>(T entity,
           Expression<Func<T, TPropType>> keySelector,
           TPropType localeValue,
           int languageId) where T : BaseEntity, ILocalizedEntity;
    }
}
