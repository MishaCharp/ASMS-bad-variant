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

        const data = await response.json();

        // �������� �������� �� ������� data
        const roleId = data.roleId;

        var existingRole = localStorage.getItem('role');
        if (existingRole !== roleId) {
            localStorage.setItem('role', roleId);
        }

        if (currentPath === '/Account/Login' && response.ok) {
            window.location.href = '/'; // �������������� �� ������� ��������, ���� ������������ ��� ����������� � �������� ����� �� �������� �����
        }
        if (currentPath === '/Home/Privacy' && response.ok && roleId != 1) {
            redirectToNoAccessPage();
        }

        return data; 

    }
    catch
    {
        redirectToLogin();
    }

}

// �������� ������� �������� ����������� ��� �������� ��������
window.addEventListener('load', checkAuth);

export { checkAuth }