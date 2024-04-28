import { fetchApi, redirectToNoAccessPage } from './api.js';

// Функция для перенаправления на страницу авторизации
function redirectToLogin() {
    window.location.href = '/Account/Login';
}

// Функция для проверки авторизации
async function checkAuth() {
    try
    {
        const currentPath = window.location.pathname;
        const response = await fetchApi('Account/Check');

        if (!response.ok) {
            redirectToLogin();
        }

        const data = await response.json();

        // Получаем значения из объекта data
        const roleId = data.roleId;

        if (currentPath === '/Account/Login' && response.ok) {
            window.location.href = '/'; // Перенаправляем на главную страницу, если пользователь уже авторизован и пытается зайти на страницу входа
        }
        if (currentPath === '/Home/Privacy' && response.ok && roleId != 1) {
            redirectToNoAccessPage();
        }

    }
    catch
    {
        redirectToLogin();
    }

}

// Вызываем функцию проверки авторизации при загрузке страницы
window.addEventListener('load', checkAuth);