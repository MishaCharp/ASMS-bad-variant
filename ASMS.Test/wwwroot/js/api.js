const API_URL = 'https://localhost:7131';

async function fetchApi(endpoint, method = 'GET', data = null, headers = {}) {
    try {
        const response = await fetch(`${API_URL}/${endpoint}`, {
            method,
            body: data ? JSON.stringify(data) : undefined,
            headers: {
                ...headers,
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('token')}`,
            },
        });

        if (!response.ok) {
            if (response.status === 401) {
                // �������� �������� ���������� ������� ��� ��������� ������ �������� ��� ���������� �����������
                redirectToNoAccessPage();
            }
        }

        return await response.json();
    } catch (error) {
        console.error('������ ��� ���������� �������:', error);
        // ��������� ������, ��������, ����� ��������� �� ������ ������������
    }
}

function redirectToNoAccessPage() {
    // ���������� ������ ��������������� �� �������� ���������� �������
    window.location.href = '/NoAccess';
}

export { fetchApi, redirectToNoAccessPage };