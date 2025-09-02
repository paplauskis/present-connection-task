const API = 'http://localhost:5148/api/packages'

export async function getAllPackages() {
  try {
    const res = await fetch(`${API}/all`)
    return await handleResponse(res)
  } catch (err) {
    return { error: err.message }
  }
}

export async function createPackage(data) {
  try {
    const res = await fetch(`${API}/create`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    })

    const contentType = res.headers.get('content-type')
    let responseData

    if (contentType && contentType.includes('application/json')) {
      responseData = await res.json()
    } else {
      responseData = await res.text()
    }

    if (!res.ok) {
      return { error: responseData || `Request failed with status ${res.status}` }
    }

    return responseData
  } catch (err) {
    return { error: err.message }
  }
}

export async function filterPackages(filter) {
  try {
    const queryParams = new URLSearchParams()
    if (filter.Status) queryParams.append('Status', filter.Status)
    if (filter.SenderName) queryParams.append('SenderName', filter.SenderName)
    if (filter.RecipientName) queryParams.append('RecipientName', filter.RecipientName)

    const res = await fetch(`${API}/filter?${queryParams.toString()}`)
    return await handleResponse(res)
  } catch (err) {
    return { error: err.message }
  }
}

export async function getPackageByTracking(trackingNumber) {
  try {
    const res = await fetch(`${API}/by-tracking-number/${trackingNumber}`)
    return await handleResponse(res)
  } catch (err) {
    return { error: err.message }
  }
}

export async function updatePackageStatus(data) {
  try {
    const res = await fetch(`${API}/update-status`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data)
    })
    return await handleResponse(res)
  } catch (err) {
    return { error: err.message }
  }
}

export async function getStatuses() {
  const res = await fetch(`${API}/statuses`)
  return res.json()
}

async function handleResponse(res) {
  const contentType = res.headers.get('content-type')
  let data

  if (contentType && contentType.includes('application/json')) {
    data = await res.json()
  } else {
    data = await res.text()
  }

  if (!res.ok) {
    throw new Error(data || `Request failed with status ${res.status}`)
  }
  return data
}
