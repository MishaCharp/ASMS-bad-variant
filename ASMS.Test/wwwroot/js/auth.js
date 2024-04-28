import { fetchApi, redirectToNoAccessPage } from './api.js';

// ������� ��� ��������������� �� �������� �����������
function redirectToLogin() {
    window.location.href = '/Account/Login';
}

// ������� ��� �������� �����������
async function checkAuth() {
    try
    {
        const currentPath = window.location.pathname;
        const response = await fetchApi('Account/Check');

        if (!response.ok) {
            redirectToLogin();
        }

        if (currentPath === '/Account/Login' && response.ok) {
            window.location.href = '/'; // �������������� �� ������� ��������, ���� ������������ ��� ����������� � �������� ����� �� �������� �����
        }

    }
    catch
    {
        redirectToLogin();
    }

}

// �������� ������� �������� ����������� ��� �������� ��������
window.addEventListener('load', checkAuth);