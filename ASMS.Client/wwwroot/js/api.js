const API_URL = 'https://localhost:7131';

async function fetchApi(endpoint, method = 'GET', data = null, headers = {}) {
    try {
        const token = `Bearer ${localStorage.getItem('token')}`;
        const response = await fetch(`${API_URL}/${endpoint}`, {
            method,
            body: data ? JSON.stringify(data) : undefined,
            headers: {
                ...headers,
                'Content-Type': 'application/json',
                'Authorization': token,
            },
        });

        if (!response.ok) {
            if (response.status === 401) {
                redirectToNoAccessPage();
            }
        }

        //console.log(response);
        return response;
    } catch (error) {
        console.error('������ ��� ���������� �������:', error);
        // ��������� ������, ��������, ����� ��������� �� ������ ������������
    }
}

function redirectToNoAccessPage() {
    // ���������� ������ ��������������� �� �������� ���������� �������
    window.location.href = '/Home/NoAccess';
}

export { fetchApi, redirectToNoAccessPage };